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
	vec3 viewDir3 = normalize(viewDir);

	vec4 colorW = material_emission;
	const float k = 0.8;
	float d1 = pow(max(dot(normal, lightDir3), 0.0), 1.0 + k);
	float d2 = pow(1.0 - dot(normal, viewDir3), 1.0 - k);

	gl_FragColor = colorW * d1 * d2;
}