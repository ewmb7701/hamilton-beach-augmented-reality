* New Project Creation
	1. Fork this repository
	2. Change the name of the product in ProductSettings/Player
	3. Modify any relevant product*specific enums, such as Joystick and ActorEngine;
* General Organization
	* Organize by "Purpose", rather than "Qualities"
	* Example: spinning red cube that is used as a MenuItem on the Main Menu;
		* Qualities: Red/Cubes/Spinning/SpinningRedCube.tiff
		* Usage: Menu/Main/Items/SpinningRedCube.tiff
	* Reasoning:
		* "Usage" is more objective, so it's easier for multiple people
		* Tends to have shorter paths
		* Groups similar objects together
			* Example: Spinning Red Menu Cube and Falling Purple Menu Sphere
				* Qualities: 
					* Red/Cubes/Spinning/SpinningRedCube.tiff
					* Purple/Sphere/Falling/FallingPurpleSphere.tiff
				* Usage: 
					* Menu/Main/Items/SpinningRedCube.tiff
					* Menu/Main/Items/FallingPurpleSphere.tiff
* Product Organization
	* <Product> is the name of the application
	* This should be changed in ProjectSettings/Player for every new product
	* There is a Resources folder for each section
	* Product Sections:
		* <Product>.App
			* Code/ assets for the compiled app / game
				* UI / UX logic, Scene management, components, etc.
			* Builds directly into the Unity project
				* Prevents hacking/ piracy without decompiling
				* With decompiled code, lacks Unity scenes/ assets
		* <Product>.Creator: 
			* For creation of DynamicAssets
		* <Product>.Engine 
			* shared Code and Assets used by both App and Creator;
			* Mostly DynamicAssets
	* Reasoning:
		* Users can create their own DynamicAssets (Creator + Engine)
		* Isolate UnityEditor code from game (App + Engine)
			* UnityEditor code cannot be in games
		* Allow for obsfucation of code
		* Easier to organize
		* Good programming practice
* Folder Organization:
	* Folders not starting with <Product> or coder handles should start with the Greek (Omega) Ω character
		* This sorts them to the bottom of alphabetical lists
		* This exclude things which are path based, like 
	* Non*script files useable by Unity go in "<Product>.<Section>.Assets"
		* Textures, Animations, Materials, Sounds, etc.
	* Folders should be compressed upwards when they become to large/ deep
		* For example:
			* Clash.Game.Assets
				* Menus
					* Items
						* Rhombi
		* Could be:
			* Clash.Game.Assets.Menus.Items
				* Rhombi
		* IMPORTANT:
			* Do NOT compress any folders with the name "Resources" or "Editor" in them!
			* These are key words Unity recognizes for specific things!
* File Organization:
	* Try to keep as few "raw" files in the Unity project
		* Having 5 different revisions of the same .sai file is completely unecessary
	* Naming Qualities
		* No spaces
		* No periods in files
		* No periods in folders (except those in Assets/)
		* Try to avoid dashes		
* Version Control
	* Git, SourceTree*, and BitBucket will be used for version control
		* *terminal/ shells/ Command line/ other software are okay, but discouraged
	* Branches
		* master:
			* Major milestones
			* No errors
		* develop:
			* "Working" revisions only
			* Merged into "master" once produce milestones have been reached
			* All relevant people must sign*off on the merge
		* feature/...:
			* Adding features
			* Feature branches have the following sub*sections
				* code
				* menus
				* actors
				* scenes
				* Probably event sub*sub*sections, like menus/css
			* Merged into "develop" after a sign*off by the head of the relevant sub*sections
	* Feature Addition Procedure:
		1. Checkout the "develop" branch and pull the latest changes
		2. Make a feature branch called "feature/<feature name>/<your handle>"
		3. Implement feature, while making periodic, error*free commits
		4. Make a pull request on the BitBucket
		5. Rebase your changes onto develop
	* DON'T FORGET TO CHECK OUT DEVELOP BEFORE WORKING ON A NEW FEATURE
* Product Performance Targets
	* Target 16:9 (1080p), 60fps!
		* No exceptions!
	* No errors!
	
Future business stuff for when there are more employees...
* Roles
	* One person can be in multiple roles!
	* Artists
		* Concept art
		* Sound
	* Engineer
		* Coding
		* Architects
	* Technician
		* Brings together Art and Code inside Unity
		* Creates content using DynamicAssets system
	* Support
		* Management
		* Project Managers
* Technologies
	* Up to you, but here are Walter's preferred:
	* Unity :^)
	* Programming:
		* IDE: Visual Studio
* Programming Standards:
	* Member Names:
		* Private/ Protected: camelCase
		* Public/ Internal: PascalCase
		* Fields: Start with _
		* Statics: End with _
		* Use  "Det" instead of "Get" if the member
			* Is "determining" something
			* Has side effects on the object
			* Takes more than O(1) time to execute
		* Don't make variables "negatives"
			* stuff like "notNull" is annoying
	* Memeber Class Order
		* Public Properties
		* EventSystem Messages
			* Direct
			* Forward
		* Public/ Internal Methods
		* Updating
			* FixedUpdate()
			* Update()
			* LateUpdate()
		* Initialization
			* Constructors
			* Init()
			* Awake()
			* OnEnable()
			* Start()
			* Reset()
		* DeInitialization
			* DeInit()
			* OnDestroy()
		* Fields, Inspector
		* Fields, Private/ Protected
		* Static Methods
		* Static Fields
		* Constants
		* Member Other
			* Avoid public/ internal members as much as you possibly can
			* Make Inspector fields Private/ Protected, with the [SerializeProperty] attribute to make them editable
	* If Blocks
		* Always make the "happy path" the first part, and the "sad path" in the else
		* If you return in the If block, return in the else block;
	* Commenting
		* Use Visual Studio's built in commenting system:
			* /// above any class member
		* Comment every line, in plain english
			* the execution path should read like a sentence of you explaining to someone who can't see the code
				* Articles/ prepositions/ punctuation is not required
					* You don't have to put an and at the very comment, for example
			* Try to keep it as high*level as possible
	* Testing
		* Writing formal tests is unnecessary and difficult with Unity
		* Do path*based testing "in your head" as you right the code
			* happy paths
			* common code paths
			* edge cases
				* "What happens if I mash every button on the joystick"?
	* Organization:
		* Namespaces:
			* Non*product namespaces should start with the programmer's handle
				* For exaple, SINe.Unity.DynamicAssets is SINe's DynamicAssets stuff for Unity
			* Product*specific namespaces should start with <Some Future Company Name Here>.<ProductName>.<ProductSection>
		* Folders:
			* Each namespace should have a folder
			* folders used for organization inside namespaces should start with Lowercase letters
		* Scripts
			* Place all static/ extension methods which _do not directly relate to a class_ in a "Utility.cs" in the namespace folder