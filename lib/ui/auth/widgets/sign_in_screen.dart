import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../view_models/signin_viewmodel.dart';

class SignInScreen extends StatefulWidget {
  const SignInScreen({super.key, required this.viewModel});
  final SignInViewModel viewModel;

  @override
  State<SignInScreen> createState() => _SignInScreenState();
}

class _SignInScreenState extends State<SignInScreen> {
  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () async {
          await widget.viewModel.signIn.execute();
          if (context.mounted) {
            if (widget.viewModel.signIn.completed) {
              context.replace(Routes.dashboard.path);
              context.pushNamed(Routes.authProfile.name);
              return;
            }

            // ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Sign in failed: $e')));
            ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Error signing in: ${widget.viewModel.signIn.error}')));
          }
        },
        child: const Text('Sign in with Microsoft'),
      ),
    );
  }
}
