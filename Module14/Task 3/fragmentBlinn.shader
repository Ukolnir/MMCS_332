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

	vec4 colorW = material_emission + material_ambient * light_ambient;
	colorW += light_diffuse * material_diffuse * max(dot(normal, lightDir3), 0.0);

	vec3 H = normalize(lightDir3 + viewDir3);

	colorW += material_specular * light_specular * pow(max(dot(normal, H), 0.0), material_shininess);
	gl_FragColor = colorW;
}