// import 'package:flutter/material.dart';

// import '../view_models/home_viewmodel.dart';

// class HomeScreen extends StatefulWidget {
//   const HomeScreen({super.key, required this.viewModel});

//   final HomeViewModel viewModel;

//   @override
//   State<HomeScreen> createState() => _HomeScreenState();
// }

// class _HomeScreenState extends State<HomeScreen> {
//   @override
//   void initState() {
//     super.initState();
//     widget.viewModel.deleteBooking.addListener(_onResult);
//   }

//   @override
//   void didUpdateWidget(covariant HomeScreen oldWidget) {
//     super.didUpdateWidget(oldWidget);
//     oldWidget.viewModel.deleteBooking.removeListener(_onResult);
//     widget.viewModel.deleteBooking.addListener(_onResult);
//   }

//   @override
//   void dispose() {
//     widget.viewModel.deleteBooking.removeListener(_onResult);
//     super.dispose();
//   }

//   @override
//   Widget build(BuildContext context) {
//     // TODO: implement build
//     throw UnimplementedError();
//   }
// }
