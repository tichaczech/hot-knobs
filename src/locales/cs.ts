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
  reason: 'Důvod', // New
  submit: 'Odeslat', // New


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
    registrationStatus: 'Stav registrace', // New
    registrationTypeLabel: 'Registrace:', // New
    status: 'Stav:', // New
    approved: 'Schváleno', // New
    pendingApproval: 'Čeká na schválení', // New
    rejected: 'Zamítnuto', // New
    cancelled: 'Zrušeno trenérem', // New
    yourStatus: 'Váš stav:', // New
    manageRegistrations: 'Spravovat registrace', // New (Trainer View)
    pending: 'Čekající', // New (Trainer View)
    registered: 'Registrovaní', // New (Trainer View)
    approve: 'Schválit', // New (Trainer Action)
    reject: 'Zamítnout', // New (Trainer Action)
    cancelRegistration: 'Zrušit registraci', // New (Trainer Action - Open Trainings)
    rejectionReasonPlaceholder: 'Volitelný důvod zamítnutí...', // New
    cancellationReasonPlaceholder: 'Volitelný důvod zrušení...', // New
    editTraining: 'Upravit trénink',
    deleteTraining: 'Smazat trénink',
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
        backToTrainings: 'Zpět na všechny tréninky', // Později změnit na zpět na Lokality
        detailsPlaceholder: 'Zde by mohly být další podrobnosti o lokalitě, jako jsou podmínky tratě, vybavení, odkaz na webové stránky atd.',
        coordinates: 'Souřadnice: {lat}, {lon}',
        mapPreview: 'Náhled mapy a odkazy',
        mapPlaceholder: '(Placeholder pro náhled mapy - Je potřeba integrace s mapovou knihovnou jako Leaflet nebo iframe)',
        mapHint: 'Prozatím použijte odkazy níže.',
        viewOnGoogleMaps: 'Zobrazit na Google Maps',
        viewOnOpenStreetMap: 'Zobrazit na OpenStreetMap',
        upcomingTrainings: 'Nadcházející tréninky zde',
        upcomingTrainingsPlaceholder: '(Funkce pro výpis tréninků pro tuto lokalitu zatím není implementována.)',
        editLocation: 'Upravit lokalitu',
        deleteLocation: 'Smazat lokalitu',
        notFound: {
            title: 'Lokalita nenalezena',
            description: 'Požadovaná lokalita nebyla nalezena.',
            return: 'Zpět na všechny tréninky', // Později změnit na zpět na Lokality
        },
        meta: {
            title: '{locationName} - Mad Sprocket Tréninková lokalita',
            description: 'Detaily tréninkové lokality: {locationName}{address}.', // Dynamicky přidat část s adresou
            addressPart: ' na adrese {address}',
            notFoundTitle: 'Lokalita nenalezena - Mad Sprocket',
        }
    },


  // Stránka Moje tréninky
  myTrainings: {
    title: 'Moje registrované tréninky',
    upcoming: 'Nadcházející',
    pending: 'Čeká na schválení', // New
    past: 'Minulé',
    noRegistrations: 'Zatím žádné registrace!',
    noRegistrationsDesc: 'Zatím jste se nezaregistrovali na žádný trénink. Přejděte na stránku <link>Dostupné tréninky</link> a nějaký si vyberte.',
    noPendingRegistrations: 'Žádné čekající registrace.', // New
    viewStatus: 'Zobrazit stav', // New (Replaces View Details for Pending)
     meta: {
        title: 'Moje registrace - Mad Sprocket',
        description: 'Zobrazte si tréninky, na které jste zaregistrováni.',
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
        skillLevelsDesc: 'Vyberte všechny relevantní úrovně dovedností pro tento trénink.',
        skillLevelsError: 'Vyberte alespoň jednu úroveň dovedností.',
        noSkillLevelsAvailableAdmin: 'Nejsou k dispozici žádné úrovně dovedností. Administrátor musí nějaké přidat.',
        invalidSkillLevel: 'Vybraná úroveň dovedností je neplatná.',
        motorcycleTypesLabel: 'Typy motocyklů',
        motorcycleTypesDesc: 'Vyberte všechny relevantní typy motocyklů pro tento trénink.',
        motorcycleTypesError: 'Vyberte alespoň jeden typ motocyklu.',
        noMotorcycleTypesAvailableAdmin: 'Nejsou k dispozici žádné typy motocyklů. Administrátor musí nějaké přidat.',
        invalidMotorcycleType: 'Vybraný typ motocyklu je neplatný.',
        descriptionLabel: 'Popis',
        descriptionPlaceholder: 'Uveďte podrobnosti o tréninku, co se jezdci naučí, jakékoliv předpoklady atd.',
        descriptionErrorShort: 'Popis musí mít alespoň 10 znaků.',
        descriptionErrorLong: 'Popis nesmí překročit 500 znaků.',
        maxRidersLabel: 'Maximální počet jezdců (Volitelné)',
        maxRidersPlaceholder: 'např. 10',
        maxRidersDesc: 'Ponechte prázdné pro neomezený počet účastníků.',
        maxRidersError: 'Musí být kladné celé číslo.',
        registrationTypeLabel: 'Typ registrace', // New
        registrationTypeDesc: 'Otevřená: Kdokoliv se může registrovat (do max). Uzavřená: Vyžaduje schválení trenérem.', // New
        registrationTypeError: 'Vyberte typ registrace.', // New
        registrationTypeOpen: 'Otevřená', // New
        registrationTypeClosed: 'Uzavřená (Vyžaduje schválení)', // New
        submitButton: 'Vytvořit trénink',
        submitButtonLoading: 'Načítání...',
        submitButtonCreating: 'Vytváření tréninku',
        successToastTitle: 'Trénink vytvořen!',
        successToastDesc: '{message}',
        errorToastTitle: 'Vytvoření selhalo',
        errorToastDesc: '{message}',
        errorLoadLocations: 'Nelze načíst lokality. Zkuste to prosím později.',
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
    registered: '{count} / {max} registrováno ({spotsLeft} {spotText} zbývá)',
    registeredOpen: '{count} registrováno (Otevřeno)',
    full: 'Plno',
    spot: 'místo',
    spots: 'místa',
    registeredBadge: 'Registrován',
    pendingBadge: 'Čeká na schválení', // New
    registrationClosed: 'Uzavř. reg.', // New
    registrationOpen: 'Otevř. reg.', // New
  },

  // Tlačítka Registrovat/Odregistrovat
  registerButton: {
    register: 'Registrovat',
    requestRegistration: 'Požádat o registraci', // New
    registrationSuccessTitle: 'Registrace úspěšná!',
    registrationSuccessDesc: '{message}',
    registrationPendingTitle: 'Žádost odeslána!', // New
    registrationPendingDesc: '{message}', // New
    registrationErrorTitle: 'Registrace selhala',
    registrationErrorDesc: '{message}',
  },
  unregisterButton: {
    unregister: 'Odregistrovat',
    withdrawRequest: 'Stáhnout žádost', // New
    confirmTitle: 'Jste si jistý?',
    confirmDesc: 'Tuto akci nelze vrátit zpět. Budete odstraněni ze seznamu registrací pro tento trénink.',
    confirmWithdrawDesc: 'Tuto akci nelze vrátit zpět. Vaše žádost o registraci bude stažena.', // New
    confirmAction: 'Potvrdit odregistraci',
    confirmWithdrawAction: 'Potvrdit stažení', // New
    unregistrationSuccessTitle: 'Odregistrace úspěšná!',
    unregistrationSuccessDesc: '{message}',
    unregistrationErrorTitle: 'Odregistrace selhala',
    unregistrationErrorDesc: '{message}',
    withdrawSuccessTitle: 'Žádost stažena', // New
    withdrawSuccessDesc: '{message}', // New
    withdrawErrorTitle: 'Stažení žádosti selhalo', // New
    withdrawErrorDesc: '{message}', // New
  },

   // Akce trenéra (Schválit/Zamítnout/Zrušit)
  trainerActions: {
    approveSuccessTitle: 'Registrace schválena',
    approveSuccessDesc: 'Jezdec úspěšně schválen.',
    approveErrorTitle: 'Schválení selhalo',
    approveErrorDesc: '{message}',
    rejectSuccessTitle: 'Registrace zamítnuta',
    rejectSuccessDesc: 'Jezdec úspěšně zamítnut.',
    rejectErrorTitle: 'Zamítnutí selhalo',
    rejectErrorDesc: '{message}',
    cancelSuccessTitle: 'Registrace zrušena',
    cancelSuccessDesc: 'Registrace jezdce úspěšně zrušena.',
    cancelErrorTitle: 'Zrušení selhalo',
    cancelErrorDesc: '{message}',
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

} as const;

