package org.example;

import java.util.Optional;

public class MageController {
    private final MageRepository repository;

    public MageController(MageRepository repository) {
        this.repository = repository;
    }
    public String find(String name) {
        Optional<Mage> mage = repository.find(name);
        if(mage.isPresent()) {
            return mage.get().toString();
        }
        else {
            return Error.NOT_FOUND.getError();
        }
    }
    public String delete(String name) {
        try {
            repository.delete(name);
            return Info.DONE.getInfo();
        }
        catch (IllegalArgumentException e) {
            return Error.NOT_FOUND.getError();
        }
    }
    public String save(MageDTO mageDTO) {
        try {
            repository.save(new Mage(mageDTO.getName(), mageDTO.getLevel()));
            return Info.DONE.getInfo();
        }
        catch (IllegalArgumentException e) {
            return Error.BAD_REQUEST.getError();
        }
    }

    public MageRepository getRepository() {
        return repository;
    }
}
