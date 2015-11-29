# Caching of geocode requests and results #

This would go a long way to helping deal with Google's 2500 request/day limit, especially during testing when we will probably be running the same track over and over again.

We could build some fuzziness into the caching system, so that you'd get a cache hit even if your lat/long values were slightly different from ones already in the cache. For instance, suppose you make a request for 1.123/4.456 and you put the result in the cache. Then, later, you make a request for 1.125/4.454. This isn't identical to the value in cache, but it may be close enough that the cache value will do so you pull the cached result rather than submit a new geocode request to Google. This would speed up processing and help keep us from hitting the 2500 limit. Of course, there would be added overhead of doing a fuzzy lookup in the cache ('is there anything in cache between 1.121-1.125/4.454-4.458?' rather than 'is 1.123/4.456 in cache?'), and it would take some trial and error to see how much fuzziness is acceptable and whether there is real benefit to using it.

# Turn correction #

It would be nice to have the ability to correct turns that are not properly detected by the algorithm. Prototype already has a pop-up window for each turn, so could view and modify in this window.

# Generate a link to a dynamic google map showing generated route with directions #

This would be a great way for someone to share the cue sheet with another rider, and to get it onto a mobile device (iPhone, Android phone) that could be used on a real ride to follow the cue sheet. Might be more useful than printing out the cue sheet and carrying it on a ride, at least for some people. User could copy/paste the link into email or make a browser bookmark.

# What else? #

Since the Google multipoint driving directions function is only available through the JavaScript/ActionScript API, it would be difficult to implement this with the client side application. The next best thing might be exporting the map image and driving directions as an HTML document. Then the user could email it to themselves to view later on a mobile device.