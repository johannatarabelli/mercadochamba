# Frontend

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.3.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Setup Guide

### Download all dependencies
Run command
```
npm install
```

### Add env variables
Add `.env` archive and run command
```
npm run envs
```

### Build frontend
Run command
```
ng serve -o
```

## Architecture
```
└── 📁app
    └── app.component.html
    └── app.component.scss
    └── app.component.spec.ts
    └── app.component.ts
    └── app.config.server.ts
    └── app.config.ts
    └── app.routes.ts
    └── 📁auth
        └── 📁components
        └── 📁guards
        └── 📁models
        └── 📁pages
        └── 📁pipes
        └── 📁services
    └── 📁core
        └── 📁components
        └── 📁guards
        └── 📁models
        └── 📁pages
        └── 📁pipes
        └── 📁services
    └── 📁material
        └── material.module.ts
    └── 📁shared
        └── 📁components
        └── 📁guards
        └── 📁models
        └── 📁pages
        └── 📁pipes
        └── 📁services
```

## Theme

### Color Pallete
| Name     | Código Hexadecimal | Color                 |
|-------------|--------------------|-----------------------------------|
| surface     | `#ffffff`          | ![#ffffff](https://via.placeholder.com/20/ffffff/000000?text=+) |
| primary     | `#15363a`          | ![#15363a](https://via.placeholder.com/20/15363a/000000?text=+) |
| secondary   | `#db5e41`          | ![#db5e41](https://via.placeholder.com/20/db5e41/000000?text=+) |
| tertiary    | `#2d394b`          | ![#2d394b](https://via.placeholder.com/20/2d394b/000000?text=+) |
| error       | `#b00020`          | ![#b00020](https://via.placeholder.com/20/b00020/000000?text=+) |
| accent      | `#db5e41`          | ![#db5e41](https://via.placeholder.com/20/db5e41/000000?text=+) |

_To get more help on color roles go check out the [M3-Material Reference](https://m3.material.io/styles/color/roles) page._

_To get more information on Angular material custom palette go to check out the [Angular material 17 - custom theme Reference](https://v17.material.angular.io/guide/theming) page._

### Typography
`fontFamily: 'Inter'`

_To get more information on font go to check out the [Google Fonts - Inter Reference](https://fonts.google.com/specimen/Inter) page._