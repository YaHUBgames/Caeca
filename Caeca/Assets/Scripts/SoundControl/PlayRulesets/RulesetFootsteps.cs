using System.Collections;
using UnityEngine;

using Caeca.TerrainControl;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl.PlayRulesets
{
    /// <summary>
    /// Plays sound left/right according to the distance moved.
    /// </summary>
    [RequireComponent(typeof(GroundedChecker))]
    public class RulesetFootsteps : PlayRuleset
    {
        [SerializeField] private GroundedChecker groundedChecker;
        [SerializeField] private Transform applyMovementOf;

        [SerializeField] private AudioSource leftLegSource;
        [SerializeField] private AudioSource rightLegSource;

        [SerializeField, Range(0, 2)]
        private float stepPitchVariation = 0.5f;
        [SerializeField, Range(-3, 3)]
        private float defaultPitch = 1f;

        [SerializeField, Range(0, 10)]
        private float averageTwoStepLength = 1;
        [SerializeField, Range(0, 1)]
        private float stepSpacing = 0.5f;


        private bool working = false;
        private Vector3 lastPosition = Vector3.zero;
        private float stepSize = 0.2f;
        private bool stepSide = false;

        private void Update()
        {
            if (!working)
                return;
            if ((lastPosition - applyMovementOf.position).magnitude < stepSize)
                return;
            lastPosition = applyMovementOf.position;
            if (stepSide)
            {
                PlayLeftLeg();
                return;
            }
            PlayRightLeg();
        }

        private void PlayLeftLeg()
        {
            leftLegSource.pitch = defaultPitch - ((stepSpacing < 0.5f) ? (1 - stepSpacing * 2) * stepPitchVariation : 0);
            StartCoroutine(PlaySteps(leftLegSource));
            stepSize = averageTwoStepLength * (1 - stepSpacing);
            stepSide = false;
        }

        private void PlayRightLeg()
        {
            rightLegSource.pitch = defaultPitch - ((stepSpacing > 0.5f) ? (stepSpacing - 0.5f) * 2 * stepPitchVariation : 0);
            StartCoroutine(PlaySteps(rightLegSource));
            stepSize = averageTwoStepLength * stepSpacing;
            stepSide = true;
        }


        public override bool CanPlaySound(float _deltaTime)
        {
            if (!GroundTypeSoundPackLoader.instance.allLoaded)
                return false;

            if (!working)
            {
                working = true;
                lastPosition = applyMovementOf.position;
            }

            groundedChecker.GetGround();

            return true;
        }

        public override int AssetTimerSetter(int _assetTimer, float _deltaTime)
        {
            return _assetTimer;
        }

        public override void PlaySound(AudioSource _source, AudioClipPack _clipPack, float _deltaTime)
        {

        }

        private IEnumerator PlaySteps(AudioSource _source)
        {
            for (int i = 0; i < (int)GroundTypes.SIZEOF; i++)
            {
                GroundTypes type = (GroundTypes)i;
                _source.PlayOneShot(GroundTypeSoundPackLoader.GetFootstepClip(type), groundedChecker.GetGroundValueOfType(type));
            }
            yield return null;
        }

        public override void OnAssetUnload(AudioSource _source)
        {
            working = false;
        }
    }
}
