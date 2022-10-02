using UnityEngine;

namespace Scenes.Games.Models
{
    public class DeadParticleCtlr : MonoBehaviour
    {
        private static DeadParticleCtlr sPrefab;

        public float timer;

        public static DeadParticleCtlr Generate(Vector3 position)
        {
            if ((object) sPrefab == null)
            {
                sPrefab = Resources.Load<DeadParticleCtlr>("Prefabs/Models/DeadParticle");
            }

            var instantiate = Instantiate(sPrefab, position, Quaternion.identity);
            instantiate.name = "DeadParticle";

            return instantiate;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > 5f)
            {
                Destroy(gameObject);
            }
        }
    }
}