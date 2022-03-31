# SirBottington2.0

The refactored version of my first Discord bot using Discord .NET.
The first bot was written when I was just learning how to code in C#, it thus had horrible design choices and structure. Since then I've learnt a lot and hopefully improved somewhat. The code as it is is ofcourse not perfect but atleast an improvement.

## Features from the old bot that has yet to be ported over are
* Roulette system
* The ATM for account credits

## SirBottington2.0 Features
* Who's That Pokemon?!
  * Renders a blacked out image of a pokemon and pastes it onto  a template background, at runtime, and posts in chat. When a correct answer has been given it posts the original image pasted onto template background.
  * Keeps tracks of how many points each user in the chat has
* Pokemon API Data
  * Search for pokemon by name or by PokeDex Id.
  * Returns an embed with information, such as type, generation, evolutions, flavor text
* Keeps track of users in a MongoDB
* XKCD Comics
  * Provides the latest XKCD comic strip in a custom embed for the chat.
  * You can search for a word and the bot will return the strip that it determines best fits the given word.
  * Get specific comic strips with ID or get a Random one.

## Plans
* Make the +help command work together with /summary/ for efficient documentation.
