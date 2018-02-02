######readme for the test task#####
the application is an API order processor.
The available "end nodes":
Orders:
api/orders/list - gets all orders in the DB //get
api/orders/order (?id=*) - gets order data by orderId //get
api/orders/order - pushes order to database, if all the items are available - marks as "Processed", in other cases - puts order on hold & sets status "Not In Stock" //post

Items:
api/items/list - lists all items in the DB //get
api/items/item (?id=*) - gets item data by itemId //get
api/items/item - updates (adds) the item data by itemId //patch
api/items/item - updates (rewrites) the item data by itemId //post


Originally app was written utilizing both EF and raw SQL, but due to problems with unit tests & resharper all logic had to be scrapped & rewritten;
due to that all unitTests were scrapped, but the final code became more clear and easier to understand

Main problems with the app:
-Poor database creation choices (product, for instance)
-lack of UnitTests