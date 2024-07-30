// Create a bumpy surface by using procedural noise to generate a height (
// displacement in normal direction).
//
// Inputs:
//   is_moon  whether we're looking at the moon or centre planet
//   s  3D position of seed for noise generation
// Returns elevation adjust along normal (values between -0.1 and 0.1 are
//   reasonable.
float bump_height( bool is_moon, vec3 s)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float h;
  if (is_moon) {
    h = improved_perlin_noise(sin(M_PI * s)) + improved_perlin_noise(0.5 * M_PI * s);
    return 0.05 * smooth_heaviside(h, 50);

  } else {
    h = improved_perlin_noise(4.0 * sin(M_PI * s));
    return 0.04 * smooth_heaviside(h, 300);
  }
  /////////////////////////////////////////////////////////////////////////////
}
