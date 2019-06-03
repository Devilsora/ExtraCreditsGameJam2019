Shader "Custom/ApplyTexture"
{
    Properties//variables
    {
      _MainTex("Main texture (RGB)", 2D) = "white" {} //allows for texture property
      _Color("Color", Color) = (1,1,1,1) //allows for color property
    }

    Subshader
    {
        Pass
        {
          CGPROGRAM //allows talk between 2 different languages - shader lab and nvidia C for graphics

          //function defines
          #pragma vertex vert //define for the building function [what shape to build it in]
          
          
          #pragma fragment frag//define for color function


          //includes
          #include "UnityCG.cginc"

          //structures

          struct appdata
          {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;

          };
          
          struct v2f
          {
            float4 pos : SV_POSITION;
            float2 uv: TEXCOORD0;

          };

          //imports - reimport from shader lab to nvidia
          float4 _Color;
          sampler2D _MainTex;


          //vertex function - builds the object
          v2f vert(appdata IN)
          {
            v2f OUT;

            OUT.pos = UnityObjectToClipPos(IN.vertex);
            OUT.uv = IN.uv;

            return OUT;
          }
          
          //fragment function - color it in
          fixed4 frag(v2f IN) : SV_Target
          {
            float4 texColor = tex2D(_MainTex, IN.uv);
            return texColor * _Color;
          }

          ENDCG
        }

        
    }
}
