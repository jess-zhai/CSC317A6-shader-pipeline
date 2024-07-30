// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 

  float theta = animation_seconds * (M_PI / 4.0);
  mat4 rotate = mat4(
  cos(theta), 0, sin(theta), 0,
  0, 1, 0, 0,
  -sin(theta), 0, cos(theta),  0,
  0, 0, 0, 1);
  mat4 translate = mat4(
    1, 0, 0, 0,
    0, 1, 0, 0,
    0, 0, 1, 0,
    6, 6, 0, 1);

  float p;
  vec3 ka, kd, ks;

  if (is_moon) {
    p = 500;
    ka = vec3(0.01);
    kd = vec3(0.4);
    ks = vec3(0.9);
  } else {
    p = 500;
    ka = vec3(0.01, 0.02, 0.05);
    kd = vec3(0.15, 0.25, 0.8);
    ks = vec3(0.9);
  }

  vec3 v = -1 * normalize(view_pos_fs_in.xyz);
  vec3 l = normalize((view * rotate * translate * vec4(0.0, 0.0, 0.0, 1.0) - view_pos_fs_in).xyz);
  vec3 n = normalize(normal_fs_in);

  color = blinn_phong(ka, kd, ks, p, n, v, l);
  /////////////////////////////////////////////////////////////////////////////
}
