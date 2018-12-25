#version 330 core
in vec2 UV;
in vec3 norm;
in vec3 lightDir;

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
    
    vec4 colorW = material_emission;
    float Ndot = max(dot(normal, lightDir3), 0.0);
    
    gl_FragColor = colorW * Ndot * texture(myTextureSampler, UV);
}