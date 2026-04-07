// import 'package:firebase_auth/firebase_auth.dart'
//   hide EmailAuthProvider;
// import 'package:firebase_ui_auth/firebase_ui_auth.dart';
// import 'package:flutter/material.dart';
// import 'package:frontend/routes/routes.dart';
// import 'package:go_router/go_router.dart';

// class SignIn extends SignInScreen {
//   const SignIn({super.key});

//   @override
//   Widget build(BuildContext context) {
//     return SignInScreen(
//       actions: [
//         AuthStateChangeAction<SignedIn>((context, state) async {
//           context.replace(Routes.dashboard.path);
//         }),
//         AuthStateChangeAction<UserCreated>((context, state) {
//           context.replace(Routes.dashboard.path);
//           context.pushNamed(Routes.authProfile.name);
//         })
//       ],
//       auth: FirebaseAuth.instance,
//       footerBuilder: (context, action) {
//         return const Padding(
//           padding: EdgeInsets.only(top: 16),
//           child: Text(
//             'By signing in, you agree to our terms and conditions.',
//             style: TextStyle(color: Colors.grey),
//           ),
//         );
//       },
//       headerBuilder: (context, constraints, shrinkOffset) {
//         return Padding(
//           padding: const EdgeInsets.all(20),
//           child: AspectRatio(
//             aspectRatio: 1,
//             child: Image.asset('assets/images/flutterfire_300x.png'),
//           ),
//         );
//       },
//       providers: [EmailAuthProvider()],
//       sideBuilder: (context, shrinkOffset) {
//         return Padding(
//           padding: const EdgeInsets.all(20),
//           child: AspectRatio(
//             aspectRatio: 1,
//             child: Image.asset('assets/images/flutterfire_300x.png'),
//           ),
//         );
//       },
//       subtitleBuilder: (context, action) {
//         return Padding(
//           padding: const EdgeInsets.symmetric(vertical: 8.0),
//           child: action == AuthAction.signIn
//               ? const Text('Welcome to Hot Knobs, please sign in!')
//               : const Text('Welcome to Hot Knobs, please sign up!'),
//         );
//       },
//     );
//   }
// }
