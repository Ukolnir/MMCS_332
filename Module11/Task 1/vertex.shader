attribute vec2 coord;
uniform mat3 scale;
void main() {
vec3 coord1 = vec3(coord, 1.0);
vec3 pos = scale*coord1;
vec2 p1 = vec2(pos);
gl_Position = vec4(p1, 0.0, 1.0);
}  