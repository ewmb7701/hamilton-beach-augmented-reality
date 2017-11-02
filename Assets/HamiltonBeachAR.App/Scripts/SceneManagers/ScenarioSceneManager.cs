using SINe.Types;
using SINe.Maths;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Playables;

using SINe.Unity.Playables;
using SINe.Unity.SceneManaging;
using SINe.Unity.DynamicAssetBundleFramework;

using Vuforia;

using SINe.Unity.Vuforia;

using AugmentEX.HamiltonBeachAR.DynamicAssetBundles;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for Scenarios;
    /// </summary>
    /// <thoughts>
    /// I could, in theory, make this a base Type with a Type param for a Scenario DAB Type;
    /// In case multiple ScenarioDAB Types;
    /// </thoughts>
    [Before(typeof(MonoBehaviour))]
    public class ScenarioSceneManager : MainTrackableSceneManager<ScenarioSceneManager>, 
        IOnMainTrackableFound, 
        IOnMainTrackableLost
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Step Methods
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Go to next step;
        /// </summary>
        public bool TryStepNext()
        {
            if (stepCanNext) // above 
            {
                stepDoNext(); // next step
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Go to previous step;
        /// </summary>
        public bool TryStepPrev()
        {
            if (stepCanPrev) // above 0
            {
                stepDoPrev(); // step prev
                return true;
            }
            else
            {
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Handlers
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void OnMainTrackableLost(TrackableBehaviour.Status previousStatus)
        {
            _playableGraph.TryStop();
        }

        public void OnMainTrackableFound(TrackableBehaviour.Status previousStatus)
        {
            _playableGraph.TryPlay();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Private
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Pauses and sets to 0;
        /// </summary>
        /// <param name="stepIndex"></param>
        private void stepAnimationStop(int stepIndex)
        {
            _mixerPlayable.GetInput(stepIndex).SetPlayState(PlayState.Paused); // pause old
            _mixerPlayable.GetInput(stepIndex).SetTime(0); // restart old
        }

        /// <summary>
        /// Start a step animation;
        /// Uses speed and normalized times;
        /// </summary>
        /// <param name="stepIndex">The index to stop;</param>
        private void stepAnimationStart(int stepIndex)
        {
            Step step = _dabStepCollection.GetStep(stepIndex); // get the step

            //Debug.LogFormat("Start Time, Normalized {0} Duration {1}", step._AnimationStartNormalized, _dabStepCollection.GetStep(stepIndex)._AnimationClipPtr.Asset.length);

            double startTime = step._AnimationStartNormalized.Denormalize(_dabStepCollection.GetStep(stepIndex)._AnimationClipPtr.Asset.length); // denormalize the start time

            //Debug.LogFormat("Start Time {0} At Speed {1}", startTime, step._AnimationSpeed);

            _mixerPlayable.GetInput(stepIndex).SetSpeed(step._AnimationSpeed); // set the speed
            _mixerPlayable.GetInput(stepIndex).SetTime(startTime); // set the start time
            _mixerPlayable.GetInput(stepIndex).SetPlayState(PlayState.Playing); // start the new clip
        }

        /// <summary>
        /// Invoke after the step changes;
        /// </summary>
        private void stepIndexOnUpdate(int stepIndexOld)
        {
            // animations update
            stepAnimationStop(stepIndexOld); // stop the old step animation
            stepAnimationStart(_stepCurrentIndex); // start the new index

            // text update
            _textStepTitle.text = stepCurrent._Title; // change text;
            _textStepDescription.text = stepCurrent._Description; // change the text

            // TODO: disable buttons here
        }

        /// <summary>
        /// Do the next step;
        /// </summary>
        private void stepDoNext()
        {
            _stepCurrentIndex++; // next step
            stepIndexOnUpdate(_stepCurrentIndex - 1); // step change
        }

        /// <summary>
        /// Do the prev step;
        /// </summary>
        private void stepDoPrev()
        {
            _stepCurrentIndex--; // prev step
            stepIndexOnUpdate(_stepCurrentIndex + 1); // on change step
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Properties, Protected
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The current step the Scenario is on;
        /// </summary>
        protected Step stepCurrent
        {
            get
            {
                return _dabStepCollection.GetStep(_stepCurrentIndex);
            }
        }

        /// <summary>
        /// Can move to the next step?
        /// </summary>
        protected bool stepCanNext
        {
            get
            {
                return _stepCurrentIndex < _stepCount - 1;
            }
        }

        /// <summary>
        /// Can move to the prev step?
        /// </summary>
        protected bool stepCanPrev
        {
            get
            {
                return _stepCurrentIndex > 0;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialized
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The Text Component to show step name;
        /// </summary>
        [SerializeField]
        private Text _textStepTitle;

        /// <summary>
        /// The Text Component to show step description;
        /// </summary>
        [SerializeField]
        private Text _textStepDescription;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        // assets

        /// <summary>
        /// The DAB for the Scenario;
        /// </summary>
        protected ScenarioDAB _dab;

        /// <summary>
        /// The StepCollection Asset;
        /// </summary>
        protected StepCollection _dabStepCollection;

        /// <summary>
        /// The number of steps;
        /// </summary>
        private int _stepCount;

        /// <summary>
        /// The index of the current step;
        /// </summary>
        private int _stepCurrentIndex = 0;

        // Playables

        /// <summary>
        /// The PlayableGraph controlling the Hologram's Animator;
        /// </summary>
        private PlayableGraph _playableGraph;

        /// <summary>
        /// Mixer playable;
        /// Input ports correspond to Step AnimationClips;
        /// </summary>
        private AnimationMixerPlayable _mixerPlayable;

        ///// <summary>
        ///// The Playable of the Mixer;
        ///// </summary>
        //private ScriptPlayable<MixerPlayableBehaviour<AnimationClipPlayable>> _mixerPlayable;

        ///// <summary>
        ///// The Behaviour of the Mixer;
        ///// </summary>
        //private MixerPlayableBehaviour<AnimationClipPlayable> _mixerBehaviour;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Start()
        {
            base.Start();

            if (_queuedScenarioDAB != null) // if there is a queued scenarioDAB
            {
                startDo(_queuedScenarioDAB); // start the queued scenario
                _queuedScenarioDAB = null; // set the queued to be null
            }
            else // no queued
            {
                throw new System.Exception("No queued ScenarioDAB! Don't start on the Scenario Scene!");
            }
        }

        /// <summary>
        /// Do the start stuff;
        /// </summary>
        private void startDo(ScenarioDAB newDAB)
        {
            _dab = newDAB; // set the scenario dab to be the queued one
            _dab.RecursiveCacheAssets(_dab.AssetBundle); // recursively cache the DAB

            _dabStepCollection = _dab.Assets.GetAsset<StepCollection>(); // cache the step collection
            _stepCount = _dabStepCollection._StepPtrs.Count; // the count steps

            // debug printouts
            //Debug.LogFormat("Step Count {0}", _stepCount);
            //foreach (string stepID in _dabStepCollection.DetStepIDS())
            //{
            //    Debug.LogFormat("Step ID {0}", stepID);
            //}

            startDo_MakePlayable(_dabStepCollection); // cache playable

            stepIndexOnUpdate(_stepCurrentIndex); // do udate for 0th

            SetRenderersEnabled(false); // disable all renderers

            _playableGraph.Play(); // play the playable
        }

        /// <summary>
        /// Create the playable for the StepCollection;
        /// </summary>
        private void startDo_MakePlayable(StepCollection stepCollection)
        {
            // create the graph
            _playableGraph = PlayableGraph.Create();
            _playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime); // set update mode

            // create the Mixer (an attempt was made with this playable script. It didn't work!)
            //AnimationClipPlayable[] animationClipPlayables = stepCollection.DetAnimationClips().CreatePlayables(_playableGraph); // create AnimationClipPlayables from the StepCollection
            //string[] stepNames = stepCollection.DetStepIDS(); // determine the step names
            //Tuple<string, AnimationClipPlayable>[] animationTuples = CollectionsUtility.ToTupleArray(stepNames, animationClipPlayables); // create the tuple array
            //_mixerPlayable = MixerPlayableBehaviour<AnimationClipPlayable>.CreateScriptPlayable(_playableGraph, animationTuples, out _mixerBehaviour); // create the mixer
            
            // pause all clips
            //foreach (AnimationClipPlayable playable in _mixerBehaviour) // for all clips
            //{
            //    playable.SetPlayState(PlayState.Paused); // pause that clip
            //}

            // create the mixer with built in stuff
            AnimationClipPlayable[] animationClipPlayables = stepCollection.DetAnimationClips().CreatePlayables(_playableGraph); // create AnimationClipPlayables from the StepCollection
            _mixerPlayable = AnimationMixerPlayable.Create(_playableGraph, animationClipPlayables.Length);
            _mixerPlayable.SetInputCount(animationClipPlayables.Length); // set input ports for mixer
            for(int sourcePort = 0; sourcePort < animationClipPlayables.Length; sourcePort++) // for each input port
            {
                _mixerPlayable.ConnectInput(sourcePort, animationClipPlayables[sourcePort], 0); // connect sourcePlayable(0)->(n)animaxtionMixer
                _mixerPlayable.SetInputWeight(sourcePort, 1); // weight of 1
                _mixerPlayable.GetInput(sourcePort).SetPlayState(PlayState.Paused); // pause the source plyable
            }

            // create PlayableOutput from Mixer->HologramAnimator
            AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(_playableGraph, "Animations", HologramAnimator); // output playables to AnimatorOutput
            animationPlayableOutput.SetSourcePlayable(this._mixerPlayable); // set the source for the output to the mixer

            GraphVisualizerClient.Show(_playableGraph, "Hologram Animator");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Dead
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _playableGraph.TryDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _playableGraph.TryPlay();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _playableGraph.TryStop();
        }

        protected override void Update()
        {
            base.Update();

            debug_NextStep();
            debug_PrevStep();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Debug
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void debug_NextStep()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryStepNext();
            }
        }

        private void debug_PrevStep()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryStepPrev();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Static
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Load the Scenario scene for a given Scenario DABID;
        /// </summary>
        /// <param name="DABID">The DAB ID for the scenario DAB;</param>
        public static bool TryLoadScenarioSceneWithScearioDABID(string DABID)
        {
            ScenarioDAB newScenario;
            if (DABCore.Inst.Database.TryOut(DABID, out newScenario)) // got scenario with DABID
            {
                _queuedScenarioDAB = newScenario; // set the queued Scenario DAB
                SceneManagerCore.Inst.LoadSceneWhenValid(ScenePID.Scenario); // load the scenario scene when should
                return true;
            }
            else // did not got DAB
            {
                return false;
            }
        }

        /// <summary>
        /// The queued scenario for this scene;
        /// </summary>
        private static ScenarioDAB _queuedScenarioDAB;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Debug
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public const string DebugDABID = "augmentex.howtousedisc.scenario";
    }
}