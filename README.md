# TublyAdventure
# This project is no longer supported or updated
Monogame C#

Last Edited: 4/9/2017

It has been a while since I have made a full attempt to create a entire functional game. I decided to start with a 2D platformer game, that will play very similiar to a classic Mario game.

### Game1.cs
This is where everything starts when the application is executed. In this class, we initialize some basic values, such as the game Camera and ScreenManager. We initialize some scenes, which of course would start off with the games MainMenuScene. After this point, the game primarily functions, communicates and changes through the SceneManager.

### SceneManager.cs
As mentioned, this is where everything is orchastrated -- after initial launch functions. This class stores a collection of game scenes as a sort of reference dictionary, and then hold another collection for scenes that are currently open and running. It was designed in to these two collections so certain scenes -- such as menus -- can be toggled, but not forgotten.

The scene manager is also added to the game instance services, so it can possibly be accessed later on for any component in the game that has access to the current game instance(Game1.cs). A scene is deemed 'open', and is added to the OpenScene collection, once it has been called to start. Conversely it is deemed 'closed' and removed once it has been called to end. All scenes must derive from the BaseScene class to be managed.

### BaseScene.cs
This class is the base of any scene we create in out game. This holds all the functions that we call in our SceneManager object. In our BaseScene object we hold 3 kinds of collections: GameObjects; DrawableComponents; and Components. This is broken up in to 3 different collections so we have the ability to manage certain game components in a predictable fashion. As for now, the GameObject collection is primarily being used, for... well... game objects. The two other collections exist because I predict using UI objects(DrawableComponents) and background objects(Components) that may also be functioning within a scene. 

When a scene is started, it is passed and Game instance, in which is the game this scene in currently running withing. The state of this scene(Open/Closed) is determiend by whether or not is currently holds a '_game' variable instance, e.g. whether it is null or not. The reason we pass the game instance to the scene is so that the scene can proactively add and remove its own component collections from our current game instance. It must also be mentioned that we hold and instance of its parent SceneManager as well. As mentioned earlier, since out SceneManager is added as a service to out game instance, we obtain it in this very way.

When a scene is restarted, we call it to end. This then removes every component it loaded from the game instance, and then clears it's collections(GameObjects, DrawableComponents, and Components). This call is made virtual so it can be overridden by any class that derives from it. For example, when we call to restart are MapScene, it will remove all the components in game and in it's collections, and then the MapScene can call to reload itself once this is complete. Simulating a restart.

By default the BaseScene is given a CheckKey function. I made this decision because most scenes will be dealing with keyboard input, and if you are familiar with XNA/Monogame framework, you will understand that getting SINGLE key inputs is very important.

Lastly, BaseScene has an update method. It must be made clear, that unlike the other objects made in this game, this update is NOT called as it would for members of the games components -- BaseScene is not derived from any component object. This update method is called by the SceneManager for all of it's open scenes. This allows us to utilize a game loop update function for our scenes. If it hasn't been clearly implied, this update method can also be overriden by classes that derive from BaseScene.

### MapScene.cs
I don't want to cover EVERY scene I created, but this one does deserve a good mention. The MapScene object is the scene used for any playable map that is loaded. In this scene, we handle our game objects such as gravity, neighboring. and minipulating changes in out BaseScene collections. Admittedly, the methods I am using for game object collisions could be a little nicer on CPU usage. Firstly, I must mention that GameObjects handle and determine their own collisions. They do this by calculating their boundaries vs other game object boundaries. They have the ability to make this comparison because of a neighboring object collection they hold(More on this in GameObject.cs) Since they require to know their neighboring objects to make this check, the MapScene acts as a synchronization helper. This MapScene loops through each game object, in which then has each object loop through the rest, to then update their neighboring objects, in which they compare boundaries for collision. This is where most of the CPU usage comes from -- undoubtingly.

Note that the MapScene also handles loading the map in which it is representing, but I will touch more on that later.
For a final mention in regards to the MapScene object, this also applies gravity to each game object while it performs the neighbor chekcing loops. It does this by applying a countinous Y vector force on the object. If this all still seems slightly confusion, perhaps you can read the 'GameObject.cs' notes and return to this reading.

### GameObject.cs
Boy, this is the real beauty of the entire project(Not sure how to imply sarcasm with text). Every game object that has a physical representation will be derived from this class. GameObject also derives from the framework object 'DrawableGameComponent', and because of this we are able to have their update and draw methods called on the game loop. GameObject holds a giant supply of properties to be accessed and minipulated: texture, position, size, velocity, boundaries, visibility, rigidness, weight etc. Since the list is fairly long, I will leave only those as a mention. Also, because it is a long list, I will only mention it's functions that I find important to understand: collision; neighbors; force; and anchor.

Collision: GameObject is a partial class, so it's collision functions can be found in 'GameObject_Collision.cs'. In this portion of the class we have multiple collision events, in which we can use to subscribe functions to when we work with other objects that derive form it, e.g. a Player object, beign derived from an Entity object, in which is derived from GameObject, can subscribe a method to the 'BottomCollided' event, to handle what should happen when it landed on top of a monster -- kill it.

Neighbors: As mention in out MapScene notes, the GameObject holds a collection of neighboring GameObjects. When the MapScene calls this game object to validate another as a neighbor, it is added to the collection. Since the game object now knows what objects neighbor it, it can check for collisions and interactions with them by looping through this collection. Neighboring bojects are determined by the 'Radius' property, in which 'NeighboringBoundary' utilizes. Neighboring boundary is, essentially, an inflated boundary of the game object itself. By default this radius is 128, so any object within 128 pixels left, right, top, and bottom is added to this collection.
Force: I don't want to spend too much time on this, because it is porrly implemented. As for now, VelocityX serves not much purpose so the 'ApplyForce' method is mainly for jumping. In a nut shell, it applys a force that is then updated in the GameObject to affect it's Y position in the world.

Anchor: This is a very important function to udnerstand. Anchor is used to determine if the object is currently landed on top of another game object. Since we are able to assign it an anchor, we can determine if gravity should be applied to it or not. Gravity is applied via the 'ApplyForce' method, and if it has an anchor, it simply ignores this force. Earlier in development, this posed a small problem for landing on game objects that then disappeard, e.g. killing a monster by landing on its head. Since anchor is stored seperately from neighboring objects, it would continue to act as if the player was landed on something even though that something no longer existed. Since anchor posed a small problem, I changed the 'ValidateAsNeighbor' method to determine if the current anchor still exists.
