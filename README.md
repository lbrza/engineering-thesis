# engineer-thesis

Internet of Things systems are scarce-resource devices that need to be protected with cryptography designed with their resource constraints in mind. Such cryptography is called "lightweight cryptography" and this project addresses the branch dedicated to hash functions.
It has many characteristics such as: processing a message of arbitrary length and obtaining a pseudo-random output of fixed length; such a function should be easy to compute, but difficult to reverse. Since the length of the output is finite, the process is prone to collisions, which must be avoided. 
A plethora of opinions of experts in the field are exposed that justify the need to practice tests for the advancement of knowledge in the field. 
Three lightweight cryptography algorithms based on the sponge construction named Photon, Quark and Spongent are selected, presenting their main characteristics and a custom development in C# programming for each one. 
A priori, the former is considered the most efficient one. Algorithms will be evaluated by carrying out successive execution tests in a high end processor as well as in an IoT device.
Processing times, the amount of instructions used and memory consumption are measured.

Keywords: Lightweight Cryptography; IoT; Cyberphysical systems; lightweight hash
functions; Photon; Quark; Spongent; Sponge construction; C#
