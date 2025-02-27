Shader "ArcheroII/UI/Gradient Ramp Add" {
	Properties {
		_MainColor ("MainColor", Vector) = (1,1,1,1)
		_MainTex ("MainTex", 2D) = "black" {}
		_GradientRamp ("Gradient Ramp", 2D) = "black" {}
		_Intensity ("Intensity", Float) = 1
		_RampScale ("Ramp Scale", Float) = 1
		_RampOffset ("Ramp Offset", Float) = 0
		_RampSpeed ("Ramp Speed", Float) = 0
		_Angle ("Angle", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Unlit/Transparent"
	//CustomEditor "ShaderForgeMaterialInspector"
}