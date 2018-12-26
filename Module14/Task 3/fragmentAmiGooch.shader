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
	vec3 normal = normalize(norm);
	vec3 lightDir3 = normalize(lightDir);
	vec3 viewDir3 = normalize(viewDir);

	vec4 surfaceColor = vec4(0.75, 0.75, 0.75, 1.0);
	vec4 warmColor = vec4(1.0, 48/255.0, 48/255.0, 1.0);
	vec4 coolColor = vec4(139/255.0, 101/255.0, 8/255.0, 1.0);
	float dWarm = 0.45;
	float dCool = 0.7;

	vec4  kCool = min(coolColor + dCool * surfaceColor, 1.0);
	vec4  kWarm = min(warmColor + dWarm * surfaceColor, 1.0);
	vec4  kFinal = mix(kCool, kWarm, dot(normal, lightDir3));

	vec3 ref = reflect(-lightDir3, normal);
	float spec = pow(max(dot(ref, viewDir3), 0.0), material_shininess);

	gl_FragColor = vec4(min(kFinal + spec, 1.0));
}