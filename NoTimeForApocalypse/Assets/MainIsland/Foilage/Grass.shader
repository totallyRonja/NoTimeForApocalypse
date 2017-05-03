// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D/Grass"
 {  
     Properties
     {
        [PerRendererData]_MainTex ("Sprite Texture", 2D) = "white" {}
		_Speed("Speed", Float) = 1
		_Intensity("Intensity", Float) = 1
		_Offset("Offset", Float) = 0
     }
     SubShader
     {
         Tags 
         { 
             "RenderType" = "Opaque" 
             "Queue" = "Transparent" 
         }
 
         Pass
         {
             ZWrite Off
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #include "UnityCG.cginc"

             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
			 
			 float _Speed;
			 float _Intensity;
			 float _Offset;

             sampler2D _MainTex;
 
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
     
                 float4 pos = v.vertex;
                 o.uv_MainTex = v.uv_MainTex;

				 pos.x += o.uv_MainTex.y*cos(_Offset + _Time * _Speed)*_Intensity;

				 o.vertex = UnityObjectToClipPos(pos);
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 half4 col = tex2D (_MainTex, IN.uv_MainTex);
                 return col;
             }
 
             ENDCG
         }
     }
 }