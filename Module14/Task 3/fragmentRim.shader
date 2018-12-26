#version 430 core
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

	vec4 rimColor = vec4(0.8, 0.0, 0.2, 1.0);
	float bias = 0.3;
	float rimPower = 8.0;

	vec3 normal = normalize(norm);
	vec3 lightDir3 = normalize(lightDir);
	vec3 viewDir3 = normalize(viewDir);

	vec3 h = lightDir + viewDir;
	h = normalize(h / length(h));

	vec4 colorW = material_emission + light_ambient* material_ambient;
	float Ndot = max(dot(normal, lightDir3), 0.0);
	colorW += material_diffuse * light_diffuse * Ndot;

	float RdotVpow = max(pow(dot(normal, h), material_shininess), 0.0);
	colorW += material_specular * light_specular * RdotVpow;

	colorW += rimColor * pow(1.0 + bias - max(dot(normal, viewDir3), 0.0), rimPower);

	gl_FragColor = colorW;
}