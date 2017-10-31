using System;

using SINe.Types;
using SINe.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Playables;

using SINe.Unity.Playables;
using SINe.Unity.AugmentedReality;
using SINe.Unity.SceneManaging;
using SINe.Unity.DynamicAssetBundleFramework;

using AugmentEX.HamiltonBeachAR.DynamicAssetBundles;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for a Scenarios;
    /// </summary>
    /// <thoughts>
    /// I could, in theory, make a base Type with a Type param for a Scenario DAB Type;
    /// In case multiple Scenario Types;
    /// </thoughts>
    [Before(typeof(MonoBehaviour))]
    public class ScenarioSceneManager : HologramSceneManager<ScenarioSceneManager>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Invoke after the step changes;
        /// </summary>
        private void onChangeStep(int indexOld, int indexNew)
        {
            // playable update
            _mixerPlayable.Pause(indexOld); // pause old index
            _mixerPlayable.Restart(indexNew); // restart the new index
            _mixerPlayable.Play(indexNew); // start the new clip

            // text update
            _textName.text = currentStep._Name; // change text;
            _textDescription.text = currentStep._Description; // change the text
        }

        /// <summary>
        /// Go to next step;
        /// </summary>
        public void StepNext()
        {
            if (_stepIndex < stepsCount - 1) // above 
            {
                _stepIndex++; // next step
                onChangeStep(_stepIndex--, _stepIndex); // step change
            }
        }

        /// <summary>
        /// Go to previous step;
        /// </summary>
        public void StepPrev()
        {
            if (_stepIndex > 0) // above 0
            {
                _stepIndex--; // prev step
                onChangeStep(_stepIndex++, _stepIndex); // on change step
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Properties, Protected
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The current step the Scenario is on;
        /// </summary>
        protected Step currentStep
        {
            get
            {
                return _dabStepCollecton._StepPtrs[_stepIndex].Asset;
            }
        }

        /// <summary>
        /// The count of steps;
        /// </summary>
        protected int stepsCount
        {
            get
            {
                return _dabStepCollecton._StepPtrs.Count;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialized
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The Text Component to show step name;
        /// </summary>
        private Text _textName;

        /// <summary>
        /// The Text Component to show step description;
        /// </summary>
        private Text _textDescription;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        // assets

        /// <summary>
        /// The DAB for the Scenario;
        /// </summary>
        protected Scenario _dab;

        /// <summary>
        /// The StepCollection Asset;
        /// </summary>
        protected StepCollection _dabStepCollecton;

        /// <summary>
        /// The number of steps;
        /// </summary>
        private int _stepCount;

        /// <summary>
        /// The index of the current step;
        /// </summary>
        private int _stepIndex = 0;

        // Playables

        /// <summary>
        /// The PlayableGraph controlling the Hologram's Animator;
        /// </summary>
        private PlayableGraph _playableGraph;

        /// <summary>
        /// The Playable of the Mixer;
        /// </summary>
        private ScriptPlayable<MixerPlayableBehaviour<AnimationClipPlayable>> _mixerPlayable;

        /// <summary>
        /// The Behaviour of the Mixer;
        /// </summary>
        private MixerPlayableBehaviour<AnimationClipPlayable> _mixerBehaviour;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Start()
        {
            base.Start();

            if (_queuedScenarioDAB != null) // if there is a queued scenarioDAB
            {
                start_cache(_queuedScenarioDAB); // start the queued scenario
                _queuedScenarioDAB = null; // set the queued to be null
            }
            else // no queued
            {
                throw new System.Exception("No queued ScenarioDAB!");
            }
        }

        /// <summary>
        /// Cache fields;
        /// </summary>
        private void start_cache(Scenario newDAB)
        {
            _dab = newDAB; // set the scenario dab to be the queued one
            _dab.RecursiveCacheAssets(_dab.AssetBundle); // recursively cache the DAB

            _dabStepCollecton = _dab.Assets.GetAsset<StepCollection>(); // cache the step collection
            _stepCount = _dabStepCollecton._StepPtrs.Count; // the count steps

            start_cache_playable(_dabStepCollecton); // cache players
        }

        /// <summary>
        /// Create the playable for the StepCollection;
        /// </summary>
        private void start_cache_playable(StepCollection stepCollection)
        {
            // create the graph
            PlayableGraph graph = PlayableGraph.Create();
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime); // set update mode

            // create the Mixer
            AnimationClipPlayable[] animationClipPlayables = stepCollection.DetAnimationClips().CreatePlayables(graph); // create AnimationClipPlayables from the StepCollection
            string[] stepNames = stepCollection.DetStepNames(); // determine the step names
            Tuple<string, AnimationClipPlayable>[] animationTuples = CollectionsUtility.ToTupleArray(stepNames, animationClipPlayables); // create the tuple array
            _mixerPlayable = MixerPlayableBehaviour<AnimationClipPlayable>.CreateScriptPlayable(graph, animationTuples, out _mixerBehaviour); // create the mixer

            // create PlayableOutput from Mixer->HologramAnimator
            AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "Animations", HologramAnimator); // output playables to AnimatorOutput
            animationPlayableOutput.SetSourcePlayable(_mixerPlayable); // set the source for the output to the mixer

            // pause all clips
            foreach(AnimationClipPlayable playable in _mixerBehaviour) // for all clips
            {
                playable.SetPlayState(PlayState.Paused); // pause that clip
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Dead
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnDisable()
        {
            _playableGraph.Destroy(); // destroy the playable graph
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Static
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Load the Scenario scene for a given Scenario DABID;
        /// </summary>
        /// <param name="DABID">The DAB ID for the scenario DAB;</param>
        public static void LoadScenarioWithScearioDAB(string DABID)
        {
            Scenario newScenario;
            if (DABCore.Inst.Database.TryGet(DABID, out newScenario)) // if 
            {
                _queuedScenarioDAB = newScenario; // set the queued Scenario DAB
                ScenesCore.Inst.LoadWhenShould(ScenePID.Scenario); // load the scenario scene when should
            }
        }

        /// <summary>
        /// The queued scenario for this scene;
        /// </summary>
        private static Scenario _queuedScenarioDAB;
    }
}