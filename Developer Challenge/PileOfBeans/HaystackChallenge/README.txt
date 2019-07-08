PROBLEM/CRITERIA

There are large piles of straw that need to be organized.
Each piece of straw has color and length data associated to it.
The business wants to organize the straws into groups of different colors, ordered by the length of the straw (shortest to longest).
Any identical pieces of straw need to be removed from the results.
The process needs to be fast, taking no longer than 30 seconds to sort a pile of ~2MM pieces of straw.


TECHNICAL REQUIREMENTS

1) MUST create an implementation of the [IHaystackOrganizer] interface.
	A) NOTE: Located in "eBags.PileOfBeans.HaystackChallenge.Organizers" namespace.
2) MUST demonstrate the execution of your [IHaystackOrganizer] implementation with Unit Tests.
	A) SHOULD use the "PileOfBeans.Test.Unit" project within the solution provided.
	B) MUST provide at least one Unit Test that demonstrates the use of the [RandomStrawFactory] in conjuction with your [IHaystackOrgnaizer] implementation.
		i) NOTE: This essentially demonstrates a performance/load test against your implementation.
3) MUST sort the collection of [Straw] into respective color-groups found on the [OrganizerResponse] object.
	A) MUST classify as "red" when the "ColorRed" value is greater than both the "ColorGreen" and "ColorBlue" values.
	B) MUST classify as "green" when the "ColorGreen" value is greater than both the "ColorRed" and "ColorBlue" values.
	C) MUST classify as "blue" when the "ColorBlue" value is greater than both the "ColorRed" and "ColorGreen" values.
	D) MUST classify as "gray" when the "ColorRed", "ColorGreen" and "ColorBlue" values are the same.
	E) When color values conflict with each other, it's your call which bucket to group the instance under.
		i) EXAMPLE: "red=200", "green=200", "blue=100", straw is neither "red-dominant" nor "green-dominant"
			so we must choose to default it to either the "red" or "green" bucket.
		ii) OPTIONAL: extend the [OrganizerResponse] to allow for more than just "reds", "greens", "blues" and "grays".
			a) Potentially detect ranges of color values for "yellows", "cyans", "magentas", etc.
4) MUST order each list of [Straw] on the [OrganizerResponse] object by the [Straw] object's "LengthInCm" value.
	A) SHOULD be ordered from shortest to longest.
5) MUST remove any [Straw] object instances that are considered to be duplicates.
	A) NOTE: duplicates are [Straw] object instances with the same "ColorRed", "ColorGreen", "ColorBlue" and "LengthInCm" values as another that has already been sorted.
6) MUST complete sorting ~2MM pieces of straw in less than 30 seconds.
	A) SHOULD make the [IHaystackOrganizer] as efficient as possible.
		i) NOTE: this is the area to really be clever; the faster the better.
	B) MAY extend the [OrganizerResponse] object to include the time it took to aquire the results.	


DELIVERABLES/NOTES

1) Write the logic within the solution provided.
2) When you are finished, zip your solution back up and send it in.
3) Try to meet as many of the requirements as possible.
4) Time box your solution to no more than a couple hours.
5) Cleverness, creativity and imagination are always a plus.
6) We want to see your development and problem solving skills.
7) Remember, there are many ways to craft this solution.
8) Show us what you can do.


HELP/TROUBLESHOOTING

-) Feel free to email us with any questions you may have.
-) Application is throwing an [OutOfMemoryException]:
	Try making the "PILE_SIZE_MIN" and "PILE_SIZE_MAX" values smaller within the [RandomStrawFactory] object
