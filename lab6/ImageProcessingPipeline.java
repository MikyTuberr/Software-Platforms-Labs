package org.example;

import org.apache.commons.lang3.tuple.Pair;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.concurrent.ForkJoinPool;
import java.util.stream.Collectors;

public class ImageProcessingPipeline {

    public static void main(String[] args) {
        if (args.length != 2) {
            System.err.println("Usage: java ImageProcessingPipeline <input_directory> <output_directory>");
            System.exit(1);
        }

        String inputDirectory = args[0];
        String outputDirectory = args[1];

        try {
            List<Path> files;
            Path source = Path.of(inputDirectory);
            try (var stream = Files.list(source)) {
                files = stream.collect(Collectors.toList());
            } catch (IOException e) {
                System.err.println("Error reading input directory: " + e.getMessage());
                return;
            }

            int[] threadPoolSizes = {2, 4, 8};
            for (int threadPoolSize : threadPoolSizes) {
                long startTime = System.currentTimeMillis();
                ForkJoinPool customThreadPool = getForkJoinPool(threadPoolSize, files, outputDirectory);
                try {
                    customThreadPool.awaitTermination(Long.MAX_VALUE, java.util.concurrent.TimeUnit.MILLISECONDS);
                } catch (InterruptedException e) {
                    System.err.println("Error waiting for thread pool termination: " + e.getMessage());
                }

                long endTime = System.currentTimeMillis();
                System.out.println("Execution time with thread pool size " + threadPoolSize + ": " + (endTime - startTime) + " ms");
            }

        } catch (Exception e) {
            System.err.println("Error: " + e.getMessage());
        }
    }

    private static ForkJoinPool getForkJoinPool(int threadPoolSize, List<Path> files, String outputDirectory) {
        ForkJoinPool customThreadPool = new ForkJoinPool(threadPoolSize);

        customThreadPool.execute(() -> {
            files.parallelStream()
                    .map(path -> Pair.of(path.getFileName().toString(), loadImage(path)))
                    .map(pair -> Pair.of(pair.getKey(), transformImage(pair.getValue())))
                    .forEach(pair -> saveImage(pair.getValue(), outputDirectory, pair.getKey()));
        });

        customThreadPool.shutdown();
        return customThreadPool;
    }

    private static BufferedImage loadImage(Path path) {
        try {
            return ImageIO.read(path.toFile());
        } catch (IOException e) {
            throw new RuntimeException("Error loading image: " + e.getMessage());
        }
    }

    private static BufferedImage transformImage(BufferedImage image) {
        BufferedImage transformedImage = new BufferedImage(image.getWidth(), image.getHeight(), image.getType());
        for (int x = 0; x < image.getWidth(); x++) {
            for (int y = 0; y < image.getHeight(); y++) {
                int rgb = image.getRGB(x, y);
                int red = (rgb >> 16) & 0xFF;
                int green = (rgb >> 8) & 0xFF;
                int blue = rgb & 0xFF;
                int newRgb = (blue << 16) | (green << 8) | red;
                transformedImage.setRGB(x, y, newRgb);
            }
        }
        return transformedImage;
    }

    private static void saveImage(BufferedImage image, String outputDirectory, String fileName) {
        try {
            File outputDir = new File(outputDirectory);
            if (!outputDir.exists()) {
                outputDir.mkdirs();
            }
            String outputPath = outputDirectory + File.separator + fileName;
            ImageIO.write(image, "jpg", new File(outputPath));
        } catch (IOException e) {
            throw new RuntimeException("Error saving image: " + e.getMessage());
        }
    }
}

