plugins {
    id("com.android.application")
    id("kotlin-android")
    // The Flutter Gradle Plugin must be applied after the Android and Kotlin Gradle plugins.
    id("dev.flutter.flutter-gradle-plugin")
    // ✅ 1. AÑADIR ESTA LÍNEA (Google Services)
    id("com.google.gms.google-services")
}

android {
    namespace = "com.example.peluqueria_aja"
    compileSdk = flutter.compileSdkVersion
    ndkVersion = flutter.ndkVersion

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_11
        targetCompatibility = JavaVersion.VERSION_11
    }

    kotlinOptions {
        jvmTarget = JavaVersion.VERSION_11.toString()
    }

    defaultConfig {
        applicationId = "com.example.peluqueria_aja"
        minSdk = flutter.minSdkVersion
        targetSdk = flutter.targetSdkVersion
        versionCode = flutter.versionCode
        versionName = flutter.versionName
    }

    buildTypes {
        release {
            signingConfig = signingConfigs.getByName("debug")
        }
    }
}

flutter {
    source = "../.."    
}

// ✅ 2. AÑADIR ESTE BLOQUE AL FINAL
dependencies {
    // Importa el BoM de Firebase para gestionar versiones automáticamente
    implementation(platform("com.google.firebase:firebase-bom:34.9.0"))
    // Añade la librería de Auth (necesaria para el login)
    implementation("com.google.firebase:firebase-auth")
}