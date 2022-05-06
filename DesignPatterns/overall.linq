<Query Kind="Statements">
  <NuGetReference>ArchPM.FluentRabbitMQ</NuGetReference>
  <NuGetReference>ArchPM.NetCore</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
</Query>

// provide, create, produce, construct
// factory method: is a creational design pattern that provides an interface for creating objects in a superclass, but allows subclasses to alter/change the type of the objects that will be created.
// abstract factory: is a creational design pattern that lets you produce families of related objects without specifiying their concrete classes.
// builder: is a creational design pattern that lets you construct complex objects step by step. the pattern allows you to produce different types and repesentations of an object using the same construction code.
// prototype: is a creational design pattern that lets you copy existing objects without making your code dependent on their classes.
// singleton: is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.

// collaborate
// adapter(aka wrapper): is a structural design pattern that allows objects with incompatiple interfaces to collaborate.
// bridge: is a structural design pattern that divides business logic or huge class into separate class hierarchies that can be developed independently.
// composite: is a structural design pattern (just “sums up” its children’s results) that lets you compose objects into tree structures and then work with these structures as if they were individual objects.
// decorator: is a structural design pattern (adds additional responsibilities ) that lets you attach !!new behaviors!! to objects by placing these objects inside special wrapper objects that contain the behaviors.
//


//Bridge: YOUR FRIEND: if you want to change the property of the class outside of the class. you have share and give the color property from the ourside as a "color class".
//Facade: !!simplified but limited interface to a complex system





// **** You can use Command to convert any operation into an object. **** --> method to class

// **** Strategy usually describes different ways of doing the same thing. **** --> method gets action from outside
// if you wanna do the same thing but in different ways, then Strategy is your friend.







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