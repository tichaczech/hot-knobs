import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../routes/routes.dart';
import '../../../utils/result.dart';
import '../l10n/localizations.dart';
import '../view_models/sign_in.dart';

class SignInScreen extends StatefulWidget {
  const SignInScreen({super.key, required this.viewModel});
  final SignInViewModel viewModel;

  @override
  State<SignInScreen> createState() => _SignInScreenState();
}

class _SignInScreenState extends State<SignInScreen> {
  @override
  Widget build(BuildContext context) {
    return ListenableBuilder(
      listenable: widget.viewModel,
      builder: (context, child) {
        final l10n = AuthLocalizations.of(context)!;
        return Scaffold(
          appBar: AppBar(actions: [], backgroundColor: Theme.of(context).colorScheme.primaryContainer, title: Text(l10n.authScreenName)),
          backgroundColor: Theme.of(context).colorScheme.secondaryContainer,
          body: SafeArea(
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      ClipRRect(
                        borderRadius: BorderRadius.circular(16),
                        child: const Image(image: AssetImage('assets/images/sign-in.png'), height: 360),
                      ),
                    ],
                  ),
                  Row(mainAxisAlignment: MainAxisAlignment.center, children: [const SizedBox(height: 16)]),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Flexible(
                        child: Text(l10n.authSignInOrSignUpToContinue, textAlign: TextAlign.center, style: Theme.of(context).textTheme.bodyLarge),
                      ),
                    ],
                  ),
                  Row(mainAxisAlignment: MainAxisAlignment.center, children: [const SizedBox(height: 16)]),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      ElevatedButton(
                        style: Theme.of(context).elevatedButtonTheme.style,
                        onPressed: () async {
                          final signIn = widget.viewModel.signIn;

                          await signIn.execute();
                          if (context.mounted) {
                            if (signIn.completed) {
                              if ((signIn.result as Ok<SignOperationResult>).value == SignOperationResult.cancelledByUser) {
                                ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(l10n.authSignInCancelledByUser)));
                                return;
                              }

                              context.replace(Routes.dashboard.path);
                              context.pushNamed(Routes.profile.name);
                              return;
                            }

                            // ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Sign in failed: $e')));
                            ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(l10n.authSignInError(signIn.error.toString()))));
                          }
                        },
                        child: Text(l10n.authFormSignInButton),
                      ),
                    ],
                  ),
                  Row(mainAxisAlignment: MainAxisAlignment.center, children: [const SizedBox(height: 16)]),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      ElevatedButton(
                        style: Theme.of(context).elevatedButtonTheme.style,
                        onPressed: () async {
                          final signUp = widget.viewModel.signUp;

                          await signUp.execute();
                          if (context.mounted) {
                            if (signUp.completed) {
                              if ((signUp.result as Ok<SignOperationResult>).value == SignOperationResult.cancelledByUser) {
                                ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(l10n.authSignUpCancelledByUser)));
                                return;
                              }

                              context.replace(Routes.dashboard.path);
                              context.pushNamed(Routes.profile.name);
                              return;
                            }

                            // ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Sign in failed: $e')));
                            ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(l10n.authSignUpError(signUp.error.toString()))));
                          }
                        },
                        child: Text(l10n.authFormSignUpButton),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }
}
