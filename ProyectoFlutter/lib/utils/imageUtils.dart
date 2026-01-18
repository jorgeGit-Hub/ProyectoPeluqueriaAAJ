import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

class ImageUtils {
  static final ImagePicker _picker = ImagePicker();

  static Future<String?> pickImageFromGallery() async {
    try {
      final XFile? pickedFile = await _picker.pickImage(
        source: ImageSource.gallery,
        maxWidth: 1024,
        maxHeight: 1024,
        imageQuality: 85,
      );

      if (pickedFile != null) {
        return getBase64FromFile(pickedFile.path);
      }
      return null;
    } catch (e) {
      debugPrint('Error al seleccionar imagen: $e');
      return null;
    }
  }

  static Future<String?> takePhoto() async {
    try {
      final XFile? pickedFile = await _picker.pickImage(
        source: ImageSource.camera,
        maxWidth: 1024,
        maxHeight: 1024,
        imageQuality: 85,
      );

      if (pickedFile != null) {
        return getBase64FromFile(pickedFile.path);
      }
      return null;
    } catch (e) {
      debugPrint('Error al tomar foto: $e');
      return null;
    }
  }

  static String getBase64FromFile(String path) {
    File file = File(path);
    List<int> fileInByte = file.readAsBytesSync();
    String fileInBase64 = base64Encode(fileInByte);
    return fileInBase64;
  }

  static Uint8List dataFromBase64String(String base64String) {
    return base64Decode(base64String);
  }

  static Widget imageFromBase64(String base64String,
      {double? width, double? height, BoxFit? fit}) {
    try {
      final bytes = dataFromBase64String(base64String);
      return Image.memory(
        bytes,
        width: width,
        height: height,
        fit: fit ?? BoxFit.cover,
        errorBuilder: (context, error, stackTrace) {
          return const Icon(Icons.broken_image, size: 50);
        },
      );
    } catch (e) {
      return const Icon(Icons.broken_image, size: 50);
    }
  }

  static bool validateImageSize(String base64String, {int maxSizeMB = 5}) {
    try {
      final bytes = dataFromBase64String(base64String);
      final sizeInMB = bytes.length / (1024 * 1024);
      return sizeInMB <= maxSizeMB;
    } catch (e) {
      return false;
    }
  }
}
