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
  reason: 'Reason',
  submit: 'Submit',

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
    registrationStatus: 'Registration Status',
    registrationTypeLabel: 'Registration:',
    yourStatus: 'Your Status:',
    manageRegistrations: 'Manage Registrations',
    allRegistrations: 'All Registrations', // New
    capacity: 'Capacity: {confirmed} / {max}', // New
    unlimitedCapacity: 'Capacity: Unlimited', // New
    waitingListCount: 'Waiting List: {count}', // New
    approve: 'Approve',
    reject: 'Reject',
    cancelRegistration: 'Cancel Registration', // Trainer cancelling confirmed rider
    rejectionReasonPlaceholder: 'Optional reason for rejection/cancellation...', // Combined placeholder
    cancellationReasonPlaceholder: 'Optional reason for cancellation...', // Kept for potential specific use
    editTraining: 'Edit Training',
    deleteTraining: 'Delete Training',
    notRegistered: 'Not registered.', // New
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
        backToTrainings: 'Back to All Trainings',
        detailsPlaceholder: 'More details about the location could go here...',
        coordinates: 'Coordinates: {lat}, {lon}',
        mapPreview: 'Map Preview & Links',
        mapPlaceholder: '(Map preview placeholder)',
        mapHint: 'For now, use the links below.',
        viewOnGoogleMaps: 'View on Google Maps',
        viewOnOpenStreetMap: 'View on OpenStreetMap',
        upcomingTrainings: 'Upcoming Trainings Here',
        upcomingTrainingsPlaceholder: '(Functionality not yet implemented.)',
        editLocation: 'Edit Location',
        deleteLocation: 'Delete Location',
        notFound: {
            title: 'Location Not Found',
            description: 'Could not find the requested location.',
            return: 'Return to All Trainings',
        },
        meta: {
            title: '{locationName} - Mad Sprocket Training Location',
            description: 'Details for the training location: {locationName}{address}.',
            addressPart: ' at {address}',
            notFoundTitle: 'Location Not Found - Mad Sprocket',
        }
    },

  // My Trainings Page
  myTrainings: {
    title: 'My Registrations',
    upcomingConfirmed: 'Upcoming Confirmed', // Updated
    pendingApproval: 'Pending Approval', // 'Created' status
    waitingList: 'On Waiting List', // 'Waiting' status
    past: 'Past Sessions', // Shows only confirmed past
    noRegistrations: 'No Registrations Yet!',
    noRegistrationsDesc: 'You haven\'t registered for any training sessions or joined any waiting lists. Head over to the <link>Available Trainings</link> page to find one.',
    noRegistrationsYet: 'No registrations yet.', // Used in trainer view
    meta: {
        title: 'My Registrations - Mad Sprocket',
        description: 'View the training sessions you are registered for, pending approval for, or on the waiting list for.', // Updated description
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
        skillLevelsDesc: 'Select all applicable skill levels.',
        skillLevelsError: 'Select at least one skill level.',
        noSkillLevelsAvailableAdmin: 'No skill levels available. An administrator needs to add some.',
        invalidSkillLevel: 'An invalid skill level was selected.',
        motorcycleTypesLabel: 'Motorcycle Types',
        motorcycleTypesDesc: 'Select all applicable motorcycle types.',
        motorcycleTypesError: 'Select at least one motorcycle type.',
        noMotorcycleTypesAvailableAdmin: 'No motorcycle types available. An administrator needs to add some.',
        invalidMotorcycleType: 'An invalid motorcycle type was selected.',
        descriptionLabel: 'Description',
        descriptionPlaceholder: 'Provide details about the training...',
        descriptionErrorShort: 'Description must be at least 10 characters.',
        descriptionErrorLong: 'Description cannot exceed 500 characters.',
        maxRidersLabel: 'Maximum Riders (Optional)',
        maxRidersPlaceholder: 'e.g., 10',
        maxRidersDesc: 'Leave blank for unlimited participants.',
        maxRidersError: 'Must be a positive whole number.',
        registrationTypeLabel: 'Registration Type',
        registrationTypeDesc: 'Open: Anyone can register (up to max). Closed: Requires trainer approval.',
        registrationTypeError: 'Select a registration type.',
        registrationTypeOpen: 'Open',
        registrationTypeClosed: 'Closed (Approval Required)',
        submitButton: 'Create Training Session',
        submitButtonLoading: 'Loading...',
        submitButtonCreating: 'Creating Training Session',
        successToastTitle: 'Training Created!',
        successToastDesc: '{message}',
        errorToastTitle: 'Creation Failed',
        errorToastDesc: '{message}',
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
    registeredCount: '{count} confirmed', // New
    waitingCount: '{count} waiting', // New
    registeredOpen: '{count} confirmed (Open)',
    full: 'Full',
    spot: 'spot', // Kept for spots left calculation if needed elsewhere
    spots: 'spots', // Kept for spots left calculation if needed elsewhere
    registrationClosed: 'Closed Reg.',
    registrationOpen: 'Open Reg.',
  },

  // Register/Cancel Buttons
  registerButton: {
    register: 'Register',
    requestRegistration: 'Request Registration', // For closed training
    joinWaitingList: 'Join Waiting List', // New
    registrationSuccessTitle: 'Registration Successful!',
    registrationSuccessDesc: '{message}',
    registrationPendingTitle: 'Registration Submitted!', // 'Created' status
    registrationPendingDesc: '{message}',
    waitingListSuccessTitle: 'Added to Waiting List!', // 'Waiting' status
    waitingListSuccessDesc: '{message}',
    registrationErrorTitle: 'Registration Failed',
    registrationErrorDesc: '{message}',
  },
  // Renamed from unregisterButton
  cancelButton: {
    cancelRegistration: 'Cancel Registration', // For 'Confirmed' status
    withdrawRequest: 'Withdraw Request', // For 'Created' status
    leaveWaitingList: 'Leave Waiting List', // For 'Waiting' status
    confirmTitle: 'Are you sure?',
    confirmDesc: 'This action cannot be undone. Your confirmed registration will be cancelled.',
    confirmWithdrawDesc: 'This action cannot be undone. Your registration request will be withdrawn.',
    confirmLeaveWaitingListDesc: 'This action cannot be undone. You will be removed from the waiting list.',
    confirmAction: 'Confirm Cancellation',
    confirmWithdrawAction: 'Confirm Withdrawal',
    confirmLeaveWaitingListAction: 'Confirm Leave',
    cancelSuccessTitle: 'Registration Cancelled!',
    cancelSuccessDesc: '{message}',
    cancelErrorTitle: 'Cancellation Failed',
    cancelErrorDesc: '{message}',
  },

   // Trainer Actions
  trainerActions: {
    approveSuccessTitle: 'Registration Approved',
    approveSuccessDesc: '{message}', // Use message from backend
    approveErrorTitle: 'Approval Failed',
    approveErrorDesc: '{message}',
    rejectSuccessTitle: 'Registration Rejected',
    rejectSuccessDesc: 'Rider rejected successfully.',
    rejectErrorTitle: 'Rejection Failed',
    rejectErrorDesc: '{message}',
    cancelSuccessTitle: 'Registration Cancelled', // Trainer cancelling confirmed
    cancelSuccessDesc: 'Rider registration cancelled successfully.',
    cancelErrorTitle: 'Cancellation Failed',
    cancelErrorDesc: '{message}',
    approveTooltip: 'Approve this rider', // New Tooltip
    rejectTooltip: 'Reject this rider', // New Tooltip
    cancelTooltip: 'Cancel this rider\'s confirmed registration', // New Tooltip
    approveWaitingFullTooltip: 'Cannot approve from waiting list, training is full', // New Tooltip
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

    // Registration Statuses - NEW
    registrationStatuses: {
        Created: 'Created', // (Pending approval for Closed)
        Confirmed: 'Confirmed',
        Waiting: 'Waiting', // (On waiting list)
        Rejected: 'Rejected',
        Cancelled: 'Cancelled',
    },

} as const;
