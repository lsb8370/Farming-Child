Shader "Custom/TransparentColorUnlit"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 0.5)
        _MainTex("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Pass
        {
            Lighting Off
            ZWrite Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            Tags {"Queue" = "Transparent"}

            Color[_Color]
            SetTexture[_MainTex] { combine texture * primary }
        }
    }

    FallBack "Unlit/Transparent"
}