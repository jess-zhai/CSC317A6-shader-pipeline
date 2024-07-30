// Given a 3d position as a seed, compute an even smoother procedural noise
// value. "Improving Noise" [Perlin 2002].
//
// Inputs:
//   st  3D seed
// Values between  -½ and ½ ?
//
// expects: random_direction, improved_smooth_step
float improved_perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec3 mininum = floor(st);
  vec3 local_point = fract(st);

  vec3 g0 = random_direction(mininum);
  vec3 g1 = random_direction(mininum + vec3(1, 0, 0));
  vec3 g2 = random_direction(mininum + vec3(1, 1, 0));
  vec3 g3 = random_direction(mininum + vec3(0, 1, 0));
  vec3 g4 = random_direction(mininum + vec3(0, 0, 1));
  vec3 g5 = random_direction(mininum + vec3(1, 0, 1));
  vec3 g6 = random_direction(mininum + vec3(1, 1, 1));
  vec3 g7 = random_direction(mininum + vec3(0, 1, 1));

  vec3 d0 = st - mininum;
  vec3 d1 = st - mininum + vec3(1, 0, 0);
  vec3 d2 = st - mininum + vec3(1, 1, 0);
  vec3 d3 = st - mininum + vec3(0, 1, 0);
  vec3 d4 = st - mininum + vec3(0, 0, 1);
  vec3 d5 = st - mininum + vec3(1, 0, 1);
  vec3 d6 = st - mininum + vec3(1, 1, 1);
  vec3 d7 = st - mininum + vec3(0, 1, 1);

  float i0 = dot(d0, g0);
  float i1 = dot(d1, g1);
  float i2 = dot(d2, g2);
  float i3 = dot(d3, g3);
  float i4 = dot(d4, g4);
  float i5 = dot(d5, g5);
  float i6 = dot(d6, g6);
  float i7 = dot(d7, g7);

  vec3 smoothed = improved_smooth_step(local_point);

  float mix_x1 = mix(i0, i1, smoothed.x);
  float mix_x2 = mix(i3, i2, smoothed.x);
  float mix_y1 = mix(mix_x1, mix_x2, smoothed.y);

  float mix_x3 = mix(i4, i5, smoothed.x);
  float mix_x4 = mix(i7, i6, smoothed.x);
  float mix_y2 = mix(mix_x3, mix_x4, smoothed.y);

  float mix_z = mix(mix_y1, mix_y2, smoothed.z);

  return mix_z / sqrt(3);
  /////////////////////////////////////////////////////////////////////////////
}

