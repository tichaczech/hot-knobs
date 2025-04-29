// src/locales/cs.ts
export default {
  // Obecné
  appName: 'Mad Sprocket',
  description: 'Spravujte a registrujte se na motokrosové a enduro tréninky s Mad Sprocket.',
  viewDetails: 'Zobrazit detaily',
  back: 'Zpět',
  loading: 'Načítání...',
  error: 'Chyba',
  success: 'Úspěch!',
  cancel: 'Zrušit',
  confirm: 'Potvrdit',
  save: 'Uložit',
  optional: 'Volitelné',
  reason: 'Důvod',
  submit: 'Odeslat',

  // Hlavička / Navigace
  nav: {
    availableTrainings: 'Dostupné tréninky',
    myRegistrations: 'Moje registrace',
    createTraining: 'Vytvořit trénink',
    loginSignUp: 'Přihlásit se / Registrovat',
    profile: '{name} ({role})',
  },

  // Domovská stránka
  home: {
    welcome: 'Vítejte v Mad Sprocket!',
    subheading: 'Vaše centrum pro motokrosové a enduro tréninky.',
    loggedInAs: 'Přihlášen jako {name} ({role}).',
    browseOrLogin: 'Prohlédněte si dostupné tréninky nebo se přihlaste pro registraci.',
    viewAllTrainings: 'Zobrazit všechny tréninky',
    upcomingTrainings: 'Nadcházející tréninky',
    seeAllTrainings: 'Zobrazit všechny dostupné tréninky',
  },

  // Stránka Tréninky
  trainings: {
    title: 'Dostupné tréninky',
    noUpcoming: 'Nebyly nalezeny žádné nadcházející tréninky. Zkuste to prosím později!',
     meta: {
        title: 'Dostupné tréninky - Mad Sprocket',
        description: 'Prohlédněte si všechny dostupné motokrosové a enduro tréninky.',
    }
  },

  // Stránka Detail tréninku
  trainingDetails: {
    backToTrainings: 'Zpět na tréninky',
    location: 'Místo konání',
    fullDescription: 'Úplný popis',
    registrationStatus: 'Stav registrace',
    registrationTypeLabel: 'Registrace:',
    yourStatus: 'Váš stav:',
    manageRegistrations: 'Spravovat registrace',
    allRegistrations: 'Všechny registrace', // New
    capacity: 'Kapacita: {confirmed} / {max}', // New
    unlimitedCapacity: 'Kapacita: Neomezená', // New
    waitingListCount: 'Čekající listina: {count}', // New
    approve: 'Schválit',
    reject: 'Zamítnout',
    cancelRegistration: 'Zrušit registraci', // Trainer cancelling confirmed rider
    rejectionReasonPlaceholder: 'Volitelný důvod zamítnutí/zrušení...', // Combined placeholder
    cancellationReasonPlaceholder: 'Volitelný důvod zrušení...', // Kept for potential specific use, but above is primary
    editTraining: 'Upravit trénink',
    deleteTraining: 'Smazat trénink',
    notRegistered: 'Nejste registrován.', // New
     notFound: {
        title: 'Trénink nenalezen',
        description: 'Požadovaný trénink nebyl nalezen.',
        return: 'Zpět na všechny tréninky',
    },
     meta: {
        title: '{trainingTitle} - Mad Sprocket Trénink',
        description: 'Detaily tréninku: {trainingTitle} dne {date} v {locationName}. Vhodné pro {skillLevels}.',
        notFoundTitle: 'Trénink nenalezen - Mad Sprocket',
    }
  },

  // Stránka Detail Lokality
   locationDetails: {
        backToTrainings: 'Zpět na všechny tréninky',
        detailsPlaceholder: 'Zde by mohly být další podrobnosti o lokalitě...',
        coordinates: 'Souřadnice: {lat}, {lon}',
        mapPreview: 'Náhled mapy a odkazy',
        mapPlaceholder: '(Placeholder pro náhled mapy)',
        mapHint: 'Prozatím použijte odkazy níže.',
        viewOnGoogleMaps: 'Zobrazit na Google Maps',
        viewOnOpenStreetMap: 'Zobrazit na OpenStreetMap',
        upcomingTrainings: 'Nadcházející tréninky zde',
        upcomingTrainingsPlaceholder: '(Funkce zatím není implementována.)',
        editLocation: 'Upravit lokalitu',
        deleteLocation: 'Smazat lokalitu',
        notFound: {
            title: 'Lokalita nenalezena',
            description: 'Požadovaná lokalita nebyla nalezena.',
            return: 'Zpět na všechny tréninky',
        },
        meta: {
            title: '{locationName} - Mad Sprocket Tréninková lokalita',
            description: 'Detaily tréninkové lokality: {locationName}{address}.',
            addressPart: ' na adrese {address}',
            notFoundTitle: 'Lokalita nenalezena - Mad Sprocket',
        }
    },


  // Stránka Moje tréninky
  myTrainings: {
    title: 'Moje registrace',
    upcomingConfirmed: 'Nadcházející potvrzené', // Updated
    pendingApproval: 'Čeká na schválení', // 'Created' status
    waitingList: 'Na čekací listině', // 'Waiting' status
    past: 'Minulé', // Shows only confirmed past
    noRegistrations: 'Zatím žádné registrace!',
    noRegistrationsDesc: 'Zatím jste se nezaregistrovali na žádný trénink ani nejste na čekací listině. Přejděte na stránku <link>Dostupné tréninky</link> a nějaký si vyberte.',
    noRegistrationsYet: 'Zatím žádné registrace.', // Used in trainer view
     meta: {
        title: 'Moje registrace - Mad Sprocket',
        description: 'Zobrazte si tréninky, na které jste zaregistrováni, čekáte na schválení nebo jste na čekací listině.', // Updated description
    }
  },

  // Stránka Vytvořit trénink
  createTraining: {
    title: 'Vytvořit nový trénink',
    description: 'Vyplňte podrobnosti pro váš nadcházející trénink.',
    form: {
        titleLabel: 'Název tréninku',
        titlePlaceholder: 'např. Pokročilé techniky zatáčení',
        titleError: 'Název musí mít alespoň 5 znaků.',
        dateLabel: 'Datum a čas',
        datePlaceholder: 'Vyberte datum a čas',
        dateError: 'Datum tréninku je povinné.',
        locationLabel: 'Místo konání',
        locationPlaceholderLoading: 'Načítání lokalit...',
        locationPlaceholder: 'Vyberte místo konání',
        locationError: 'Musíte vybrat místo konání.',
        locationNotAvailable: 'Nejsou k dispozici žádné lokality',
        noLocationsAvailableAdmin: 'Nejsou k dispozici žádné lokality. Administrátor musí nějaké přidat.',
        skillLevelsLabel: 'Úrovně dovedností',
        skillLevelsDesc: 'Vyberte všechny relevantní úrovně dovedností.',
        skillLevelsError: 'Vyberte alespoň jednu úroveň dovedností.',
        noSkillLevelsAvailableAdmin: 'Nejsou k dispozici žádné úrovně. Administrátor musí nějaké přidat.',
        invalidSkillLevel: 'Vybraná úroveň dovedností je neplatná.',
        motorcycleTypesLabel: 'Typy motocyklů',
        motorcycleTypesDesc: 'Vyberte všechny relevantní typy motocyklů.',
        motorcycleTypesError: 'Vyberte alespoň jeden typ motocyklu.',
        noMotorcycleTypesAvailableAdmin: 'Nejsou k dispozici žádné typy. Administrátor musí nějaké přidat.',
        invalidMotorcycleType: 'Vybraný typ motocyklu je neplatný.',
        descriptionLabel: 'Popis',
        descriptionPlaceholder: 'Uveďte podrobnosti o tréninku...',
        descriptionErrorShort: 'Popis musí mít alespoň 10 znaků.',
        descriptionErrorLong: 'Popis nesmí překročit 500 znaků.',
        maxRidersLabel: 'Maximální počet jezdců (Volitelné)',
        maxRidersPlaceholder: 'např. 10',
        maxRidersDesc: 'Ponechte prázdné pro neomezený počet.',
        maxRidersError: 'Musí být kladné celé číslo.',
        registrationTypeLabel: 'Typ registrace',
        registrationTypeDesc: 'Otevřená: Kdokoliv se může registrovat (do max). Uzavřená: Vyžaduje schválení trenérem.',
        registrationTypeError: 'Vyberte typ registrace.',
        registrationTypeOpen: 'Otevřená',
        registrationTypeClosed: 'Uzavřená (Vyžaduje schválení)',
        submitButton: 'Vytvořit trénink',
        submitButtonLoading: 'Načítání...',
        submitButtonCreating: 'Vytváření tréninku',
        successToastTitle: 'Trénink vytvořen!',
        successToastDesc: '{message}',
        errorToastTitle: 'Vytvoření selhalo',
        errorToastDesc: '{message}',
        errorLoadData: 'Nelze načíst potřebná data pro formulář. Zkuste to prosím později.',
    },
     meta: {
        title: 'Vytvořit trénink - Mad Sprocket',
        description: 'Vytvořte nový motokrosový nebo enduro trénink.',
    }
  },

  // Komponenta Karta tréninku
  trainingCard: {
    taughtBy: 'Vede {trainerName}',
    levels: 'Úrovně:',
    bikes: 'Motorky:',
    registeredCount: '{count} potvrzeno', // New
    waitingCount: '{count} čeká', // New
    registeredOpen: '{count} potvrzeno (Otevřeno)',
    full: 'Plno',
    spot: 'místo', // Kept for spots left calculation if needed elsewhere
    spots: 'místa', // Kept for spots left calculation if needed elsewhere
    registrationClosed: 'Uzavř. reg.',
    registrationOpen: 'Otevř. reg.',
  },

  // Tlačítka Registrovat/Zrušit
  registerButton: {
    register: 'Registrovat',
    requestRegistration: 'Požádat o registraci', // For closed training
    joinWaitingList: 'Přidat se na čekací listinu', // New
    registrationSuccessTitle: 'Registrace úspěšná!',
    registrationSuccessDesc: '{message}',
    registrationPendingTitle: 'Žádost odeslána!', // 'Created' status
    registrationPendingDesc: '{message}',
    waitingListSuccessTitle: 'Přidán na čekací listinu!', // 'Waiting' status
    waitingListSuccessDesc: '{message}',
    registrationErrorTitle: 'Registrace selhala',
    registrationErrorDesc: '{message}',
  },
  // Renamed from unregisterButton
  cancelButton: {
    cancelRegistration: 'Zrušit registraci', // For 'Confirmed' status
    withdrawRequest: 'Stáhnout žádost', // For 'Created' status
    leaveWaitingList: 'Opustit čekací listinu', // For 'Waiting' status
    confirmTitle: 'Jste si jistý?',
    confirmDesc: 'Tuto akci nelze vrátit zpět. Vaše potvrzená registrace bude zrušena.',
    confirmWithdrawDesc: 'Tuto akci nelze vrátit zpět. Vaše žádost o registraci bude stažena.',
    confirmLeaveWaitingListDesc: 'Tuto akci nelze vrátit zpět. Budete odebráni z čekací listiny.',
    confirmAction: 'Potvrdit zrušení',
    confirmWithdrawAction: 'Potvrdit stažení',
    confirmLeaveWaitingListAction: 'Potvrdit opuštění',
    cancelSuccessTitle: 'Registrace zrušena!',
    cancelSuccessDesc: '{message}',
    cancelErrorTitle: 'Zrušení selhalo',
    cancelErrorDesc: '{message}',
  },

   // Akce trenéra
  trainerActions: {
    approveSuccessTitle: 'Registrace schválena',
    approveSuccessDesc: '{message}', // Use message from backend
    approveErrorTitle: 'Schválení selhalo',
    approveErrorDesc: '{message}',
    rejectSuccessTitle: 'Registrace zamítnuta',
    rejectSuccessDesc: 'Jezdec úspěšně zamítnut.',
    rejectErrorTitle: 'Zamítnutí selhalo',
    rejectErrorDesc: '{message}',
    cancelSuccessTitle: 'Registrace zrušena', // Trainer cancelling confirmed
    cancelSuccessDesc: 'Registrace jezdce úspěšně zrušena.',
    cancelErrorTitle: 'Zrušení selhalo',
    cancelErrorDesc: '{message}',
    approveTooltip: 'Schválit tohoto jezdce', // New Tooltip
    rejectTooltip: 'Zamítnout tohoto jezdce', // New Tooltip
    cancelTooltip: 'Zrušit potvrzenou registraci tohoto jezdce', // New Tooltip
    approveWaitingFullTooltip: 'Nelze schválit z čekací listiny, trénink je plný', // New Tooltip
  },

  // Úrovně dovedností
  skillLevels: {
    Beginner: 'Začátečník',
    Intermediate: 'Středně pokročilý',
    Advanced: 'Pokročilý',
    Pro: 'Profesionál',
  },

  // Typy motocyklů
  motorcycleTypes: {
    '125cc': '125cc',
    '250cc': '250cc',
    '450cc': '450cc',
    Electric: 'Elektrická',
    Other: 'Jiná',
  },

   // Role uživatelů
    userRoles: {
        rider: 'Jezdec',
        trainer: 'Trenér',
        guest: 'Host',
    },

    // Typy registrace
    registrationTypes: {
        open: 'Otevřená',
        closed: 'Uzavřená (Vyžaduje schválení)',
    },

    // Stavy registrace - NEW
    registrationStatuses: {
        Created: 'Vytvořeno', // (Čeká na schválení u Uzavřené)
        Confirmed: 'Potvrzeno',
        Waiting: 'Čekající', // (Na čekací listině)
        Rejected: 'Zamítnuto',
        Cancelled: 'Zrušeno',
    },

} as const;
