// import 'package:firebase_ui_auth/firebase_ui_auth.dart';
// import 'package:flutter/material.dart';

// class Profile extends ProfileScreen {
//   const Profile({super.key});

//   @override
//   Widget build(BuildContext context) {

//     // currentUser.reauthenticateWithCredential(EmailAuthProvider.credential(
//     //   email: currentUser.email!,
//     //   password: 'dummyPassword',
//     // ));

//     // TODO: Add password update functionality
//     // TODO: Add Firestore integration

//     return ProfileScreen(
//       actions: [
//         SignedOutAction((context) {
//           Navigator.of(context).popUntil((route) => route.isFirst);
//         }),
//       ],
//       appBar: AppBar(
//         backgroundColor: Theme.of(context).colorScheme.primaryContainer,
//         title: const Text('User Profile'),
//       ),
//       deleteConfirmation: (context) => Future.value(false),
//       showDeleteConfirmationDialog: true,
//       children: [
//         const SizedBox(height: 20),
//         Text(
//           'Welcome, User',
//           style: Theme.of(context).textTheme.headlineMedium,
//         ),
//         const SizedBox(height: 20),
//       ],
//     );
//   }
// }
