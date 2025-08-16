# Lightweight cryptography for Internet of Things devices

Computer Engineering (Bachelor's and master's degree), Universidad Argentina de la Empresa - UADE. December 2021.

## About the thesis

The thesis focuses on profiling lightweight cryptography. The pioneers in this field, such as the Photon, Quark, and Spongent algorithms, were developed prior to the NIST call for algorithm standardization. A software implementation was created for each algorithm in C#. Subsequently, several tests were conducted on an IoT device, such as a Raspberry Pi.

The evaluation involves recording execution times after multiple iterations, comparing the code size of each algorithm, analyzing the number of lines of intermediate language, and evaluating memory usage at various key points. 

The work was part of an official university research project that included other cryptographic subjects. The objective was to create a theoretical testing framework that can be reused in the future.

Keywords: Lightweight Cryptography; IoT; Cyberphysical systems; lightweight hash functions; Photon; Quark; Spongent; Sponge construction; C#

## About this repository

This repository contains the C# implementations of three lightweight cryptographic hash functions—Photon, Quark, and Spongent—as part of a Computer Engineering thesis. The project also includes a profiler to benchmark the performance of these algorithms on both high-performance and resource-constrained IoT devices.

**Table of Contents**

[TOCM]

[TOC]

## 📜 Summary

The Internet of Things (IoT) consists of resource-scarce devices that require specialized, lightweight cryptography for security. This project focuses on lightweight hash functions, which process messages of arbitrary length to produce a fixed-length, pseudo-random output. The core challenge is to create a function that is easy to compute but difficult to reverse, while minimizing the risk of collisions.

Three sponge-construction-based algorithms were selected for this study:

* Photon: Designed for hardware efficiency, particularly in RFID tags. It is based on AES-like permutations.
* Quark: A compact function inspired by the Grain stream cipher and the Katan block cipher.
* Spongent: Based on the PRESENT block cipher, designed to be compact and efficient.

The C# implementations were evaluated on a high-performance desktop and a Raspberry Pi 3 Model B+. The performance metrics included processing time, instruction count, and memory consumption. The results confirmed the initial hypothesis that Photon is the most efficient of the three algorithms, demonstrating superior performance in execution time and memory usage, especially on the resource-constrained Raspberry Pi.

## 🚀 Usage Guide

This guide assumes you have the .NET Core SDK installed on your system. The implementations were developed using Visual Studio 2019 and .NET Core 2.1.

### 1. Project Structure

The source code is organized as follows:

`Photon/`: Contains the C# project for the Photon hash function.

`Quark/`: Contains the C# project for the Quark hash function.

`Spongent/`: Contains the C# project for the Spongent hash function.

`Profiler/`: Contains the C# project for the performance profiler.

Each algorithm's project directory contains three main files:

`Program.cs`: The main driver for the application.

`(algorithm-name).cs`: The core implementation of the hash function.

`Constants.cs`: Contains all the predefined constants for the algorithm.

### 2. Compiling and Running the Algorithms

To compile and run each hash function individually, navigate to its respective directory and use the dotnet run command.

Example for Photon:

`cd Photon/`
`dotnet run`

This will execute the Main function in Program.cs, which will hash a sample message and print the resulting digest to the console.

### 3. Using the Profiler

The profiler is designed to measure the performance of each algorithm across various input message lengths.

Step-by-step instructions:

#### Navigate to the Profiler Directory:

`cd Profiler/`

#### Configure the Profiler:
Open the `Program.cs` file in the `Profiler/` directory. You can adjust the following parameters:

- **CYCLES**: The number of times to run the hash function for each message length to get an average execution time. The default is 1000.

- **Message Lengths**: The profiler is pre-configured to test a range of message lengths from 0 to 1,000,000 characters. You can modify the loops in the `GetAllRandomStrings` function to change these ranges.

#### Run the Profiler:
Execute the profiler using the dotnet run command. It's recommended to run it with high priority to get more accurate timing results, especially on systems running other processes.

##### On Windows (using Command Prompt):

`> start /REALTIME dotnet run`

##### On Linux:

`$ sudo nice -n -20 dotnet run`

#### View the Results:
The profiler will output a table to the console for each algorithm, showing:

- Input string length.
- Execution time for a single run.
- Total execution time for all cycles.
- Average execution time per run.
- Calculated hash rate (hashes per second).

### 4. Running on a Raspberry Pi (or other IoT device)

To run the code on a different architecture like the ARM processor in a Raspberry Pi, you need to publish the application for that specific runtime.

#### Install .NET Core SDK on the Device:
Follow the official Microsoft documentation to install the .NET Core SDK for your device's architecture (e.g., Linux ARM64).

#### Publish the Application:
On your development machine, publish the project for the target runtime. For example, to publish the profiler for a 64-bit ARM Linux system:

`$ cd Profiler/`
`$ dotnet publish -c Release -r linux-arm64`

This will create a self-contained application in the `bin/Release/netcoreapp2.1/linux-arm64/publish/` directory.

#### Transfer and Run:
Copy the contents of the publish directory to your Raspberry Pi. You can use tools like `scp` or `rsync`. Once transferred, make the main executable file runnable and execute it.

##### On the Raspberry Pi
`$ chmod +x Profiler # Or the name of your main executable`
`$ ./Profiler`

