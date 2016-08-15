##Hello and welcome to the PageParse demo application.

##Why?
The purpose of this application is to demonstrate code and development practices though the creation of a small web aplication.

##Ok, what does it do?
Targeting an arbitrary webpage, this app will:
 - Count all user visable words (including alt and title attributes) and display a list of the 10 most commonly used with their frequency
 - Extract and display all images linked directly by the page.

##What assumptions did you make?
That's a great question, Here are the assumptions I thought worth calling out explicitly:
 - 'Words' may contain, or be entireley composed of digits
 - The user only wants to ever see 10 words, even when words #11-n are tied with #10 in frequency
 - IFramed content doesnt count. Words or images.
 - All images are acessable to any user. No special checks or security exist around retreiving the images.
 - All images and words to process exist on first load of the page, and are not asynchronusly loaded later.

##Neat, how do I use it?
Well, there's the "I'm a developer with VS2015", which is known to work, and the "Install it for real" way, which is untested, as I dont curenlty have accress to an IIS server :/.

"I'm a developer":
Pull the repository down from GitHub, open it in Visual Studio 2015, and hit Ctrl+F5 (run without debugging).

"I'm using a real server"
 - Make sure you have IIS and .NET 4.5.2 installed on the target machine.
 - Download the app from https://github.com/mgoetz/PageParse/blob/Reconstruction/PageParse_1.0_MattGoetz.rar
 - Unzip the contents to a folder in your prefered location for running websites
 - Create a new site in IIS and point it at the location where you unpacked the rar file
 - *Update the application pool created for the IIS site to .NET 4.0
 
##Why didnt you host this on Azure?
Good question.  I'm currenlty working on deploying it there, but have not yet succeeded.  When I do, you will be able to explore the app at http://parsepage-goetzonline.azurewebsites.net/
