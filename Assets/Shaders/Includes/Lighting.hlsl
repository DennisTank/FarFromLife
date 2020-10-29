#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction,out float1 Attenuation, out float3 Color) {
#if SHADERGRAPH_PREVIEW
	Direction = 10;
    Color = 1;
	Attenuation = 1;
#else
	
	Light mainLight = GetMainLight();
	Color = mainLight.color;
	Direction = mainLight.direction;
	Attenuation = mainLight.distanceAttenuation;
	

	float4 shadowCoord;
	#ifdef LIGHTWEIGHT_SHADOWS_INCLUDED
		#if SHADOWS_SCREEN
			float4 clipPos = TransformWorldToHClip(WorldPos);
			shadowCoord = ComputeScreenPos(clipPos);
		#else
			shadowCoord = TransformWorldToShadowCoord(WorldPos);
		#endif
	#endif
	
#endif
}

#endif