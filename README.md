# CrewOnDemand
 
Hello

the data file configuration for the Api project is configured in the Api's 
appsettings.json file, here: 
"jsoncrewbookings": "CrewBookings.json",
"jsoncrew": "Crew.json"

likewise, the configuration for the Integration Test project is in that project's 
appsettings.test.json file, here:
"jsoncrewbookings": "CrewBookings.json",
"jsoncrew": "Crew.json"

data access should all work out of the box, but I'm documenting this in case you run 
into any unexpected problems. data is read from the json files, but not written to them.

in this project I have tried to cover various bases, and give a sense
of end-to-end, from the POST Api action method through to the logic of fairly selecting a pilot, 
and then Mocking the sending of a notification to the selected pilot ... (I imagine that it triggers a 
push notification to their mobile device, informing them of the just-booked job).

this api has 3 endpoints: 
you can check the plumbing using /healthcheck.
you can get a dummy flight booking via /api/flightbookings/{id} - there is a little seed data.
you can post to /api/flightbookings to see the pilot selection logic working - here's a sample booking:

{"id": 200,
"StartLocation": "Munich",
"EndLocation": "Berlin",
"Departure": "2020-05-01T09:00:00Z",
"Return": "2020-05-01T11:00:00Z",
"Duration": 2}

Thank you, Mel.
