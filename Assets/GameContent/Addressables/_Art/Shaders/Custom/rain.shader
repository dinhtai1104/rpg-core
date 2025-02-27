Shader "rain " {
	Properties {
		_yvshuikuandu ("yvshuikuandu", Vector) = (20,0.3,0,0)
		_yvshuidaxiao ("yvshuidaxiao", Float) = 3.7
		_yvshuisudu ("yvshuisudu", Float) = 3
		_yansetoumingdu ("yanse/toumingdu", Vector) = (0.4198113,0.9649187,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	//CustomEditor "ASEMaterialInspector"
}