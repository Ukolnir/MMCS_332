#version 330 core
in vec2 UV;
in vec3 norm;
in vec3 lightDir;
in vec3 viewDir;
in float vert_distance;

uniform vec4 light_position;
uniform vec4 light_ambient;
uniform vec4 light_diffuse;
uniform vec4 light_specular;
uniform vec3 light_attenuation;

uniform vec4 material_ambient;
uniform vec4 material_diffuse;
uniform vec4 material_specular;
uniform vec4 material_emission;
uniform float material_shininess;
uniform sampler2D myTextureSampler;

uniform int use;
in vec4 FragmentColor;
void main() {
	vec3 normal = normalize(norm);
	vec3 lightDir3 = normalize(lightDir);

	float diff = 0.2 + max(dot(norm, lightDir3), 0.0);
	
	vec4 colorW;

	if (diff < 0.4)
		colorW = material_emission * 0.3;
	else if(diff < 0.7)
		colorW = material_emission * 0.6;
	else
		colorW = material_emission;

	gl_FragColor = colorW;
}