attribute vec3 coord;
attribute vec3 color;
varying vec3 var_color;
uniform mat4 matrix;
void main() {
    gl_Position = matrix * vec4(coord, 1.0);
    var_color = color;
}