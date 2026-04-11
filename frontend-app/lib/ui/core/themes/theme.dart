import 'package:flutter/material.dart';

abstract final class AppTheme {
  static InputDecorationTheme _inputDecorationTheme = InputDecorationTheme(border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)));

  static OutlinedButtonThemeData _outlinedButtonThemeData = OutlinedButtonThemeData(
    style: ButtonStyle(
      padding: WidgetStateProperty.all<EdgeInsets>(const EdgeInsets.all(12)),
      shape: WidgetStateProperty.all<RoundedRectangleBorder>(
        RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
        ),
      ),
    ),
  );

  static ThemeData lightTheme = ThemeData(
    colorScheme: ColorScheme.fromSeed(seedColor: Colors.orange, brightness: Brightness.light, dynamicSchemeVariant: DynamicSchemeVariant.fidelity),
    inputDecorationTheme: _inputDecorationTheme,
    outlinedButtonTheme: _outlinedButtonThemeData    
  );

  static ThemeData darkTheme = ThemeData(
    colorScheme: ColorScheme.fromSeed(seedColor: Colors.orange, brightness: Brightness.dark, dynamicSchemeVariant: DynamicSchemeVariant.fidelity),
    inputDecorationTheme: _inputDecorationTheme,
    outlinedButtonTheme: _outlinedButtonThemeData
  );
}
