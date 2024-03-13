# Laboratory 1: Collections

## Overview
This lab focuses on basic operations on data collections and sorting mechanisms.

## Tasks
1. Implement a model class with specified fields and methods.
   
2. Implement sorting using both natural order and an alternative criterion.
  
3. Generate a test set with specified characteristics.
   
4. Print the set while maintaining the recursive structure.
   
5. Generate statistics on the number of descendants.

# Laboratory 2: Threads

## Overview
This lab focuses on basic mechanisms for launching and synchronizing threads.

## Tasks
1. Prepare a shared resource for submitting and retrieving tasks. The resource should allow adding and retrieving tasks without removing the previous ones if not yet processed. Utilize the wait-notify mechanism for thread synchronization.

2. Prepare a shared resource for collecting computation results. Adding a new result should not remove the previous ones. Utilize critical section mechanisms.

3. Implement threads to perform defined computations. These threads should continuously retrieve tasks from the task queue resource, execute them, and store the results in the result collection resource.

4. Upon starting the application, launch the appropriate number of threads for complex computations. The number of threads depends on the command-line parameter. Users should be able to submit new tasks via the console.

5. Users should have the ability to close the application via the console. Upon termination, all threads should gracefully terminate.

# Laboratory 3: Network Sockets

## Overview
This lab focuses on basic mechanisms for handling input-output and network communication using sockets.

## Tasks
1. Implement proper handling of incoming connections and delegate their processing to new threads.

2. Implement establishing a connection from the client side.

3. Implement the specified binary protocol.

