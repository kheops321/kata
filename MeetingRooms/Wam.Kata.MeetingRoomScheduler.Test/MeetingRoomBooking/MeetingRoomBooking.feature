Feature: Book meeting rooms
	In order to book meeting rooms
	As a simple user
	I want to be able to perform book operations over an api

@t_room @t_get
Scenario: Get all rooms
	When I ask for the meeting room list
	Then we must obtain a http OK result
	And we must obtains 10 rooms
	And all room names must be unique

@t_meeting @t_delete  
Scenario: Cancel meeting on unknown room
	When I try to delete a meeting using an unknown room
	Then we must obtain an http BadRequest result
	
@t_meeting @t_delete  
Scenario: Cancel unknown meeting on a valid room
	When I try to delete an unknown meeting on a valid room
	Then we must obtain an http NotFound result

@t_meeting @t_delete
Scenario: Cancel known meeting on a valid room
	Given We have the following meetings on our database
	| MeetingCode							|
	| fb0267fe-aa39-496e-bd97-4db7501fbe9a	|
	| ecf98d1b-af63-409a-8b3e-d87fbdb963e9	|
	When I try to delete the meeting fb0267fe-aa39-496e-bd97-4db7501fbe9a
	Then we must obtain an http OK result
	
@t_meeting @t_add
Scenario: Add meeting with invalid data
	When I try to add a meeting with invalid data
	Then we must obtain a BadRequest result

@t_meeting @t_add
Scenario: Add meeting on unknown room
	When I try to add a meeting on an unknown room
	Then we must obtain a http BadRequest result
	
@t_meeting @t_add
Scenario: Add meeting on known room on already booked slot
	Given We have the following bookings on room2 for today
	| StartHour | EndHour |
	| 0			| 6	      |
	| 10        | 12      |
	| 14        | 16      |
	| 18        | 24      |
	When I try to add a meeting on same room  and starting at 13 and ending at 15
	Then we must obtain an http Conflict result
	And the result must contains 8 available slots
	And the available slots must like following
	| StartHour | EndHour |
	| 6         | 7       |
	| 7         | 8       |
	| 8         | 9       |
	| 9         | 10      |
	| 12        | 13      |
	| 13        | 14      |
	| 16        | 17      |
	| 17        | 18      |

@t_meeting @t_add
Scenario: Add meeting on known room on free slot
	Given We have the these bookings on room2 for today
	| StartHour | EndHour |
	| 0			| 6	      |
	| 18        | 24      |
	When I try to add a meeting on the same room starting at 8 and ending at 10 on same day
	Then we must obtain an http Created result
	And we should obtain the code of the created meeting which must be a valid guid
