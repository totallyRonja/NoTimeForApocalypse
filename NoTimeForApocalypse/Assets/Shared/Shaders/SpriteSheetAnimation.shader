﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D/SpriteSheetAnimation"
{  
    Properties
    {
        [PerRendererData]_MainTex ("Sprite Texture", 2D) = "white" {}
        _Sprites("X amount| Y amount| empty| time(optional)", Vector) = (1, 1, 0, 0)
        _FrameProperties("start| end| fps| offset", Vector) = (0, 0, 10, 0)
        _Flashing("Flashing", Range(0, 1)) = 0
        _FlashingColor("flashing Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "Queue" = "Transparent"
            "DisableBatching" = "True"
        }

        Pass
        {
		    ZWrite Off
			Cull Off
            Blend SrcAlpha OneMinusSrcAlpha 
  
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
			 
			float4 _Sprites;
            float4 _FrameProperties;

            sampler2D _MainTex;

            float _Flashing;
            float4 _FlashingColor;
 
            struct Vertex
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };
     
            struct Fragment
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };
  
            Fragment vert(Vertex v)
            {
                Fragment o;
                    
                float4 localPos = v.vertex;
                localPos.x /= _Sprites.x;
                localPos.y /= _Sprites.y;

                o.vertex = UnityObjectToClipPos(localPos);
                o.uv_MainTex = v.uv_MainTex;

                return o;
            }
                                                     
            float4 frag(Fragment IN) : COLOR
            {
                float time = _Sprites.w == 0 ? _Time.y : _Sprites.w;
                float frame = (_FrameProperties.x + (((( -time + _FrameProperties.w) 
                    * _FrameProperties.z)%(_FrameProperties.y - _FrameProperties.x))+(_FrameProperties.y - _FrameProperties.x))%(_FrameProperties.y - _FrameProperties.x));
                float2 uv = IN.uv_MainTex;
                uv.x = (uv.x / _Sprites.x) + ((floor(frame) % _Sprites.x) / _Sprites.x);
                uv.y = ((uv.y / _Sprites.y) + (floor(frame / _Sprites.x) / _Sprites.y));

                half4 map = tex2D (_MainTex, uv);
                float alpha = map.a;
                map = half4(lerp(map,_FlashingColor,_Flashing).rgb, map.a);
                return map;
            }
 
            ENDCG
        }
    }
}