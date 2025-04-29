// src/locales/en.ts
export default {
  // General
  appName: 'Mad Sprocket',
  description: 'Manage and register for motocross and enduro trainings with Mad Sprocket.',
  viewDetails: 'View Details',
  back: 'Back',
  loading: 'Loading...',
  error: 'Error',
  success: 'Success!',
  cancel: 'Cancel',
  confirm: 'Confirm',
  save: 'Save',
  optional: 'Optional',
  reason: 'Reason', // New
  submit: 'Submit', // New

  // Header / Navigation
  nav: {
    availableTrainings: 'Available Trainings',
    myRegistrations: 'My Registrations',
    createTraining: 'Create Training',
    loginSignUp: 'Login / Sign Up',
    profile: '{name} ({role})',
  },

  // Home Page
  home: {
    welcome: 'Welcome to Mad Sprocket!',
    subheading: 'Your hub for motocross and enduro training sessions.',
    loggedInAs: 'Logged in as {name} ({role}).',
    browseOrLogin: 'Browse available trainings or log in to register.',
    viewAllTrainings: 'View All Trainings',
    upcomingTrainings: 'Upcoming Training Sessions',
    seeAllTrainings: 'See all available trainings',
  },

  // Trainings Page
  trainings: {
    title: 'Available Training Sessions',
    noUpcoming: 'No upcoming training sessions found. Check back soon!',
    meta: {
        title: 'Available Trainings - Mad Sprocket',
        description: 'Browse all available motocross and enduro training sessions.',
    }
  },

  // Training Details Page
  trainingDetails: {
    backToTrainings: 'Back to Trainings',
    location: 'Location',
    fullDescription: 'Full Description',
    registrationStatus: 'Registration Status', // New
    registrationTypeLabel: 'Registration:', // New
    status: 'Status:', // New
    approved: 'Approved', // New
    pendingApproval: 'Pending Approval', // New
    rejected: 'Rejected', // New
    cancelled: 'Cancelled by Trainer', // New
    yourStatus: 'Your Status:', // New
    manageRegistrations: 'Manage Registrations', // New (Trainer View)
    pending: 'Pending', // New (Trainer View)
    registered: 'Registered', // New (Trainer View)
    approve: 'Approve', // New (Trainer Action)
    reject: 'Reject', // New (Trainer Action)
    cancelRegistration: 'Cancel Registration', // New (Trainer Action - Open Trainings)
    rejectionReasonPlaceholder: 'Optional reason for rejection...', // New
    cancellationReasonPlaceholder: 'Optional reason for cancellation...', // New
    editTraining: 'Edit Training', // Keep for future
    deleteTraining: 'Delete Training', // Keep for future
    notFound: {
        title: 'Training Not Found',
        description: 'Could not find the requested training session.',
        return: 'Return to All Trainings',
    },
    meta: {
        title: '{trainingTitle} - Mad Sprocket Training',
        description: 'Details for the training session: {trainingTitle} on {date} at {locationName}. Suitable for {skillLevels}.',
        notFoundTitle: 'Training Not Found - Mad Sprocket',
    }
  },

    // Location Details Page
    locationDetails: {
        backToTrainings: 'Back to All Trainings', // Consider a back to Locations page later
        detailsPlaceholder: 'More details about the location could go here, such as track conditions, amenities, website link, etc.',
        coordinates: 'Coordinates: {lat}, {lon}',
        mapPreview: 'Map Preview & Links',
        mapPlaceholder: '(Map preview placeholder - Integration with a map library like Leaflet or an iframe is needed here)',
        mapHint: 'For now, use the links below.',
        viewOnGoogleMaps: 'View on Google Maps',
        viewOnOpenStreetMap: 'View on OpenStreetMap',
        upcomingTrainings: 'Upcoming Trainings Here',
        upcomingTrainingsPlaceholder: '(Functionality to list trainings for this location is not yet implemented.)',
        editLocation: 'Edit Location',
        deleteLocation: 'Delete Location',
        notFound: {
            title: 'Location Not Found',
            description: 'Could not find the requested location.',
            return: 'Return to All Trainings', // Consider a back to Locations page later
        },
        meta: {
            title: '{locationName} - Mad Sprocket Training Location',
            description: 'Details for the training location: {locationName}{address}.', // Dynamically add address part
            addressPart: ' at {address}',
            notFoundTitle: 'Location Not Found - Mad Sprocket',
        }
    },

  // My Trainings Page
  myTrainings: {
    title: 'My Registered Trainings',
    upcoming: 'Upcoming Sessions',
    pending: 'Pending Approval', // New
    past: 'Past Sessions',
    noRegistrations: 'No Registrations Yet!',
    noRegistrationsDesc: 'You haven\'t registered for any training sessions. Head over to the <link>Available Trainings</link> page to find one.',
    noPendingRegistrations: 'No pending registrations.', // New
    viewStatus: 'View Status', // New (Replaces View Details for Pending)
    meta: {
        title: 'My Registrations - Mad Sprocket',
        description: 'View the training sessions you are registered for.',
    }
  },

  // Create Training Page
  createTraining: {
    title: 'Create New Training Session',
    description: 'Fill in the details for your upcoming training session.',
    form: {
        titleLabel: 'Training Title',
        titlePlaceholder: 'e.g., Advanced Cornering Techniques',
        titleError: 'Title must be at least 5 characters.',
        dateLabel: 'Date and Time',
        datePlaceholder: 'Pick a date and time',
        dateError: 'A date for the training is required.',
        locationLabel: 'Location',
        locationPlaceholderLoading: 'Loading locations...',
        locationPlaceholder: 'Select a location',
        locationError: 'A location must be selected.',
        locationNotAvailable: 'No locations available',
        noLocationsAvailableAdmin: 'No locations available. An administrator needs to add some.',
        skillLevelsLabel: 'Skill Levels',
        skillLevelsDesc: 'Select all applicable skill levels for this training.',
        skillLevelsError: 'Select at least one skill level.',
        noSkillLevelsAvailableAdmin: 'No skill levels available. An administrator needs to add some.',
        invalidSkillLevel: 'An invalid skill level was selected.',
        motorcycleTypesLabel: 'Motorcycle Types',
        motorcycleTypesDesc: 'Select all applicable motorcycle types for this training.',
        motorcycleTypesError: 'Select at least one motorcycle type.',
        noMotorcycleTypesAvailableAdmin: 'No motorcycle types available. An administrator needs to add some.',
        invalidMotorcycleType: 'An invalid motorcycle type was selected.',
        descriptionLabel: 'Description',
        descriptionPlaceholder: 'Provide details about the training, what riders will learn, any prerequisites, etc.',
        descriptionErrorShort: 'Description must be at least 10 characters.',
        descriptionErrorLong: 'Description cannot exceed 500 characters.',
        maxRidersLabel: 'Maximum Riders (Optional)',
        maxRidersPlaceholder: 'e.g., 10',
        maxRidersDesc: 'Leave blank for unlimited participants.',
        maxRidersError: 'Must be a positive whole number.', // Zod handles type coercion, message for invalid input
        registrationTypeLabel: 'Registration Type', // New
        registrationTypeDesc: 'Open: Anyone can register (up to max). Closed: Requires trainer approval.', // New
        registrationTypeError: 'Select a registration type.', // New
        registrationTypeOpen: 'Open', // New
        registrationTypeClosed: 'Closed (Approval Required)', // New
        submitButton: 'Create Training Session',
        submitButtonLoading: 'Loading...',
        submitButtonCreating: 'Creating Training Session',
        successToastTitle: 'Training Created!',
        successToastDesc: '{message}', // Placeholder for server message
        errorToastTitle: 'Creation Failed',
        errorToastDesc: '{message}', // Placeholder for server message
        errorLoadLocations: 'Could not load locations. Please try again later.',
        errorLoadData: 'Could not load necessary form data. Please try again later.',
    },
     meta: {
        title: 'Create Training - Mad Sprocket',
        description: 'Create a new motocross or enduro training session.',
    }
  },

  // Training Card Component
  trainingCard: {
    taughtBy: 'Taught by {trainerName}',
    levels: 'Levels:',
    bikes: 'Bikes:',
    registered: '{count} / {max} registered ({spotsLeft} {spotText} left)',
    registeredOpen: '{count} registered (Open)',
    full: 'Full',
    spot: 'spot',
    spots: 'spots',
    registeredBadge: 'Registered',
    pendingBadge: 'Pending Approval', // New
    registrationClosed: 'Closed Reg.', // New (Short for Closed Registration)
    registrationOpen: 'Open Reg.', // New (Short for Open Registration)
  },

  // Register/Unregister Buttons
  registerButton: {
    register: 'Register',
    requestRegistration: 'Request Registration', // New (for closed trainings)
    registrationSuccessTitle: 'Registration Successful!',
    registrationSuccessDesc: '{message}',
    registrationPendingTitle: 'Registration Submitted!', // New
    registrationPendingDesc: '{message}', // New
    registrationErrorTitle: 'Registration Failed',
    registrationErrorDesc: '{message}',
  },
  unregisterButton: {
    unregister: 'Unregister',
    withdrawRequest: 'Withdraw Request', // New
    confirmTitle: 'Are you sure?',
    confirmDesc: 'This action cannot be undone. You will be removed from the registration list for this training session.',
    confirmWithdrawDesc: 'This action cannot be undone. Your registration request will be withdrawn.', // New
    confirmAction: 'Confirm Unregistration',
    confirmWithdrawAction: 'Confirm Withdrawal', // New
    unregistrationSuccessTitle: 'Unregistration Successful!',
    unregistrationSuccessDesc: '{message}',
    unregistrationErrorTitle: 'Unregistration Failed',
    unregistrationErrorDesc: '{message}',
    withdrawSuccessTitle: 'Registration Withdrawn', // New
    withdrawSuccessDesc: '{message}', // New
    withdrawErrorTitle: 'Withdrawal Failed', // New
    withdrawErrorDesc: '{message}', // New
  },

  // Trainer Actions (Approve/Reject/Cancel)
  trainerActions: {
    approveSuccessTitle: 'Registration Approved',
    approveSuccessDesc: 'Rider approved successfully.',
    approveErrorTitle: 'Approval Failed',
    approveErrorDesc: '{message}',
    rejectSuccessTitle: 'Registration Rejected',
    rejectSuccessDesc: 'Rider rejected successfully.',
    rejectErrorTitle: 'Rejection Failed',
    rejectErrorDesc: '{message}',
    cancelSuccessTitle: 'Registration Cancelled',
    cancelSuccessDesc: 'Rider registration cancelled successfully.',
    cancelErrorTitle: 'Cancellation Failed',
    cancelErrorDesc: '{message}',
  },

  // Skill Levels
  skillLevels: {
    Beginner: 'Beginner',
    Intermediate: 'Intermediate',
    Advanced: 'Advanced',
    Pro: 'Pro',
  },

  // Motorcycle Types
  motorcycleTypes: {
    '125cc': '125cc',
    '250cc': '250cc',
    '450cc': '450cc',
    Electric: 'Electric',
    Other: 'Other',
  },

    // User Roles
    userRoles: {
        rider: 'Rider',
        trainer: 'Trainer',
        guest: 'Guest',
    },

     // Registration Types
    registrationTypes: {
        open: 'Open',
        closed: 'Closed (Approval Required)',
    },

} as const;

