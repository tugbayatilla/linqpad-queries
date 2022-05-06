<Query Kind="Expression">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>



////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////// STRUCTURAL //////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////
/// ADAPTER
/// Adapter is commonly used with an existing app to make some otherwise-incompatible classes work together nicely.
/// Adapter: !!Connects 2 existing systems and let them work together!!
///////////////////////////


///////////////////////////
/// BRIDGE
/// Bridge: YOUR FRIEND: if you want to change the property of the class outside of the class. you have share and give the color property from the outside as a "color class".
/// Bridge is usually designed up-front, letting you develop parts of an application independently of each other. 
/// Bridge: !!Extend the functionality(properties) of the existing class with other independent classes, every property is another class !!
///////////////////////////


///////////////////////////
/// COMPOSITE
///////////////////////////


///////////////////////////
/// DECORATOR
///////////////////////////


///////////////////////////
/// FACADE
/// !!simplified but limited interface to a complex system
///////////////////////////


///////////////////////////
/// FLYWEIGHT
///////////////////////////


///////////////////////////
/// PROXY
///////////////////////////










////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////// BEHAVIORAL //////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Command  : You can use Command to convert any operation into an object. **** --> method to class
//Strategy :
// **** Strategy usually describes different ways of doing the same thing. **** --> method gets action from outside
// if you wanna do the same thing but in different ways, then Strategy is your friend.


//scenario: you have a class have multiple methods or one method seperating 
//Command: multiple methods doing different actions were converted to classes : file creation, mail sending, sms sending, (sending can be done with strategy also) 
//Strategy: a method doing same thing with different ways was converted to classes. file creation in different environments, exp: in linux, in windows, in ios

// Behaviour
// Command to convert any operation into an object. 
// Strategy usually !!describes different ways of doing the same thing!!, letting you swap these algorithms within a single context class.
// Decorator lets you change the skin of an object, while Strategy lets you change the guts.


/*

	Command and Strategy may look similar because you can use both to parameterize an object with some action. 
	However, they have very different intents.

	You can use Command to convert any operation into an object. 
	The operation’s parameters become fields of that object. 
	The conversion lets you defer execution of the operation, queue it, store the history of commands, send commands to remote services, etc.

	On the other hand, 
	Strategy usually describes different ways of doing the same thing, 
	letting you swap these algorithms within a single context class.

*/