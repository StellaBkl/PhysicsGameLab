# PhysicsGameLab
### Student Assessment AR App

**PhysicsGameLab** is an Android app developed with Unity and C# that aims to assess students through augmented reality (AR) games linked to quizzes created by their teachers. The app utilizes **Vuforia** to recognize image targets and display AR content, enhancing the learning experience. All student data, including quiz grades and personal information, is stored in **Firebase**, making it easily accessible for teachers from their dedicated app [PhysicsGameLabforTeachers](https://github.com/StellaBkl/PhysicsGameLab).

The app is designed for students in 5th grade to engage in interactive AR games as part of their assessments, contributing to a more dynamic and fun learning process.

## Features

### 1. **User Authentication**
- Students receive login credentials from their teachers (username and student ID).
- They must sign up by filling in the student ID and creating a password.
- Once registered, students can log in using their username and newly created password.

### 2. **Home Page**
- After logging in, students are taken to the **Main Menu**, where they can navigate through the app's features.

### 3. **Main Menu**
The main menu provides the following options:

- **Profile**:
  - Students can view their personal profile, including:
    - Points accumulated from completed quizzes.
    - Basic personal information.
    - Option to change their password.

- **Quizzes**:
  - Students can access quizzes assigned by their teacher.
  - Each quiz contains multiple AR-based exercises (games) that students must complete to receive a final grade for the quiz.
  - Quizzes are fetched from **Firebase**, and only **enabled quizzes** created by the teacher are visible to students.

- **Scoreboard**:
  - Students can view the leaderboard, where they can see their scores compared to their classmates (or other 5th-grade students).
  - This feature allows students to compete for the highest points and encourages friendly competition.

### 4. **AR Games**
- Quizzes contain **AR exercises** powered by **Vuforia**.
  - Students must use their camera to recognize image targets, triggering interactive AR content.
  - Successfully completing the AR games is part of the assessment, and points are awarded based on performance.

### 5. **Firebase Integration**
- All quiz grades and student data (including points) are stored in **Firebase**.
- Teachers can view students' scores and progress through the **PhysicsGameLabforTeachers** app.

### 6. **Login/Logout**
- Students can log in using their credentials, and they can log out when finished.
- They can revisit the app at any time and continue from where they left off.

## Technologies Used
- **Unity**: Game engine used to develop the app and manage AR content.
- **C#**: The primary programming language used for scripting in Unity.
- **Vuforia**: AR platform used for recognizing image targets and displaying AR content for quizzes.
- **Firebase**: Backend service for user authentication, storing student data, and managing quiz scores.

## Installation

### Prerequisites
- Install **Unity** and set up the Android build environment.
- Set up a **Firebase** project and configure authentication and Firestore Database.
- **Vuforia SDK** should be imported into the Unity project for AR functionality.

