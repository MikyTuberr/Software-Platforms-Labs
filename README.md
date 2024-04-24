# Programming Laboratories

## Laboratory 1: Collections

### Overview
This lab focuses on basic operations on data collections and sorting mechanisms.

### Tasks
1. Implement a model class with specified fields and methods.
2. Implement sorting using both natural order and an alternative criterion.
3. Generate a test set with specified characteristics.
4. Print the set while maintaining the recursive structure.
5. Generate statistics on the number of descendants.

## Laboratory 2: Threads

### Overview
This lab focuses on basic mechanisms for launching and synchronizing threads.

### Tasks
1. Prepare a shared resource for submitting and retrieving tasks. The resource should allow adding and retrieving tasks without removing the previous ones if not yet processed. Utilize the wait-notify mechanism for thread synchronization.
2. Prepare a shared resource for collecting computation results. Adding a new result should not remove the previous ones. Utilize critical section mechanisms.
3. Implement threads to perform defined computations. These threads should continuously retrieve tasks from the task queue resource, execute them, and store the results in the result collection resource.
4. Upon starting the application, launch the appropriate number of threads for complex computations. The number of threads depends on the command-line parameter. Users should be able to submit new tasks via the console.
5. Users should have the ability to close the application via the console. Upon termination, all threads should gracefully terminate.

## Laboratory 3: Network Sockets

### Overview
This lab focuses on basic mechanisms for handling input-output and network communication using sockets.

### Tasks
1. Implement proper handling of incoming connections and delegate their processing to new threads.
2. Implement establishing a connection from the client side.
3. Implement the specified binary protocol.

## Laboratory 4: Java Persistence API (Hibernate) and H2

### Overview
This lab covers the Java Persistence API (JPA) with Hibernate and the H2 database.

### Tasks
1. Implement entity classes with specified fields and annotations.
2. Configure Hibernate for entity persistence.
3. Create and execute queries using JPA.
4. Use the H2 in-memory database for testing and development.

## Laboratory 5: Unit Testing with JUnit and Mockito

### Overview
This lab focuses on unit testing with JUnit and Mockito.

### Tasks
1. Write unit tests for the implemented classes and methods.
2. Utilize JUnit for writing test cases.
3. Use Mockito for mocking dependencies and verifying interactions.

## Laboratory 6: Parallel Operations with Stream API

### Overview
This lab explores parallel operations using the Stream API.

### Tasks
1. Implement parallel operations on collections using Stream API.
2. Compare performance between sequential and parallel operations.
3. Handle synchronization and potential issues with parallel streams.

## Laboratory 7: File Operations in C#

### Overview
This lab focuses on file operations using C#.

### Tasks
1. Implement basic file read and write operations.
2. Handle different file formats and encodings.
3. Perform file manipulation tasks such as copying, moving, and deleting.

## Laboratory 8: WPF File TreeView

### Overview
This lab involves creating a file tree view using Windows Presentation Foundation (WPF).

### Tasks
1. Design a WPF application with a TreeView control.
2. Populate the TreeView with file system data.
3. Implement functionalities such as expanding and collapsing nodes.
4. Handle user interactions and events on the TreeView.
