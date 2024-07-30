// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float theta = animation_seconds * M_PI / 4.0;

  mat4 translate = mat4(
    1, 0, 0, 0,
    0, 1, 0, 0,
    0, 0, 1, 0,
    6, 6, 0, 1);

  mat4 rotate = mat4(
    cos(theta), 0, sin(theta), 0,
    0, 1, 0, 0,
    -sin(theta), 0, cos(theta), 0,
    0, 0, 0, 1);

  vec4 light = view * rotate * translate * vec4(0.0, 0.0, 0.0, 1.0);

  vec3 T, B;
  tangent(normalize(sphere_fs_in), T, B);
  vec3 b_sphere_fs_in = bump_position(is_moon, sphere_fs_in);
  vec3 perceived_n = cross((bump_position(is_moon, sphere_fs_in + T * 0.0001) - bump_position(is_moon, sphere_fs_in)) / 0.0001, 
  	(bump_position(is_moon, sphere_fs_in + B * 0.0001) - bump_position(is_moon, sphere_fs_in)) / 0.0001);

  if (dot(sphere_fs_in, perceived_n) < 0){
    perceived_n = -perceived_n;
  }
  vec3 bumped_normal = (transpose(inverse(view)) * transpose(inverse(model(is_moon, animation_seconds))) * vec4(normalize(perceived_n), 1.0)).xyz;

  vec3 ka, kd;
  vec3 ks = vec3(0.9);
  float p;
  vec3 n, v, l;
  
  if (is_moon) {
    ka = vec3(0.02);
    kd = vec3(0.5);
    p = 500;
  } else {
    ka = vec3(0.02, 0.03, 0.06);
    kd = vec3(0.15, 0.25, 0.8);
    p = 1000;
  }

  n = normalize(bumped_normal);
  v = -normalize(view_pos_fs_in.xyz);
  l = normalize(light.xyz - view_pos_fs_in.xyz);

  color = blinn_phong(ka, kd, ks, p, n, v, l);
  /////////////////////////////////////////////////////////////////////////////
}
