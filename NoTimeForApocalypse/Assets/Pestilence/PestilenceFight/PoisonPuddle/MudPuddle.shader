// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D/Dissolve"
{  
    Properties
    {
		[PerRendererData]_MainTex ("Sprite Texture", 2D) = "white" {}
		_DissolveTex ("Dissolve Texture", 2D) = "white" {}
		_Size("dissolve size", float) = 1
    }
    SubShader
    {
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha 

        Tags 
        { 
            "RenderType" = "Opaque" 
            "Queue" = "Transparent" 
        }
 
        Pass
        {

            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
			 
			float _Size;

            sampler2D _MainTex;
			sampler2D _DissolveTex;
 
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
     
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.uv_MainTex;

                return o;
            }
                                                     
            float4 frag(Fragment IN) : COLOR
            {
				half4 color = tex2D (_MainTex, IN.uv_MainTex);
				half4 dissolve = tex2D (_DissolveTex, IN.uv_MainTex);
				
				color.a -= dissolve < (1 - _Size);
				
                return color;
            }
 
            ENDCG
        }
    }
}