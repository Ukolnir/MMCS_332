#version 330 core
layout(location = 0) in vec3 coord;
layout(location = 1) in vec2 vertexUV;
layout(location = 2) in vec3 normal;

uniform mat4 transform_model;
uniform mat4 transform_viewProjection;
uniform mat3 transform_normal;
uniform vec3 transform_viewPosition;

uniform vec4 light_position;

out vec2 UV;
out vec3 norm;
out vec3 lightDir;
out vec3 viewDir;
out float vert_distance;
void main(){
    vec4 vertex = transform_model * vec4(coord, 1.0);
    vec4 lightDir4 = light_position - vertex;
    gl_Position = transform_viewProjection * vertex;

    UV = vertexUV;
    norm = normalize(transform_normal * normal);
    lightDir = normalize(vec3(lightDir4));
    viewDir = normalize(transform_viewPosition - vec3(vertex));
    vert_distance = length(lightDir4);
}