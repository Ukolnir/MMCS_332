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
    
    float attenuation = 1.0;
    if (length(light_attenuation) != 0)
        attenuation = 1.0 / (light_attenuation[0] + light_attenuation[1] * vert_distance + light_attenuation[2] * vert_distance * vert_distance);
    vec4 colorW = material_emission;
    colorW += material_ambient * light_ambient * attenuation;
    float Ndot = max(dot(normal, lightDir3), 0.0);
    colorW += material_diffuse * light_diffuse * Ndot * attenuation;
    float RdotVpow = max(pow(dot(reflect(-lightDir, normal), viewDir3), material_shininess), 0.0);
    colorW += material_specular * light_specular * RdotVpow * attenuation;
    
    gl_FragColor = colorW * texture(myTextureSampler, UV);
}