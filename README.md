# Difference two Collections <img src="./DifferenceCollection.svg" style="width:60px;"/>

With this function it is possible to determine the difference between Collection. The result is returned as another collection of type <DifferenceResultItem>.

#### Example
<img src="./Source_1.png" style="width:800px;"/>

#### Result
<img src="./Source_2.png" style="width:600px;"/>

## Functionality
The class DifferenceCollection is passed two collections with one type as parameters which inherit from the interface "ISyncItem".
Within the DifferenceCollection class, two comparators are used to check which of the items in the passed collection is new, removed, or changed. For this purpose a collection of the type <DifferenceResultItem> is returned.


### Class Interface
<img src="./Source_3.png" style="width:600px;"/>
