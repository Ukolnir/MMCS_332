attribute vec3 coord;
uniform mat4 matrix;
void main() {
gl_Position = matrix * vec4(coord, 1.0);
}  